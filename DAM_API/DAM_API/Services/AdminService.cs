using AutoMapper;
using DAM_API.Data;
using DAM_API.Helper;
using DAM_API.Models;
using DAM_API.Models.Dtos;
using DAM_API.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<Utilizador> _userManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AdminService(UserManager<Utilizador> userManager, AppDbContext context, IConfiguration configuration, IMapper mapper, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
            _env = env;

        }

        public async Task<Response> CriarCantinaAsync(CantinaDto viewModel)
        {
            if(viewModel == null)
            {
                return new Response
                {
                    Message = "Ocorreu um erro ao adicionar a cantina",
                    IsSuccess = false
                };
            }

            try
            {
                Cantina c = _mapper.Map<Cantina>(viewModel);

                c.Foto = await Images.SaveImage(c.FotoFile, _env, "Cantinas");

                _context.Cantinas.Add(c);
                await _context.SaveChangesAsync();

                return new Response
                {
                    Message = "Cantina adicionada com sucesso!",
                    IsSuccess = true
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new Response
            {
                Message = "Ocorreu um erro ao adicionar a cantina",
                IsSuccess = false
            };
        }

        public async Task<ReturnUtilizadoresDto> GetUtilizadoresAsync()
        {
            ReturnUtilizadoresDto utilizadores = new ReturnUtilizadoresDto
            {
                Utilizadores = new List<Utilizador>()
            };

            var listTotalUsers = await _context.Utilizadores.Include(x=>x.Admin).Include(x=>x.AdminCantina).Include(x=>x.Customer).ToListAsync();

            foreach (var user in listTotalUsers)
            {
                if(_userManager.IsInRoleAsync(user, "Cliente").Result || _userManager.IsInRoleAsync(user, "AdminCantina").Result)
                {
                    utilizadores.Utilizadores.Add(user);
                }
            }                   

            if (utilizadores.Utilizadores != null)
            {
                utilizadores.IsSuccess = true;
                utilizadores.Message = "Utilizadores recebidos com sucesso!";
                return utilizadores;
            }

            utilizadores.IsSuccess = false;
            utilizadores.Message = "Ocorreu um erro ao receber os utilizadores";
            return utilizadores;
        }

        public async Task<Response> RemoverSuspensaoAsync(string userId)
        {
                if (userId == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Erro desbloquear o cliente."
                    };
                }

                var userDesbloquear = _context.Utilizadores.FirstOrDefault(x => x.Id == userId);
                var suspensao = _context.Suspensões.FirstOrDefault(x => x.IdU == userId);
                var removerFaltasCustomer = _context.Customers.FirstOrDefault(x=>x.IdC == userId);

                try
                {
                removerFaltasCustomer.Faltas = 0;
                    userDesbloquear.Suspenso = false;
                    _context.Suspensões.Remove(suspensao);
                    _context.Update(removerFaltasCustomer);
                    _context.Utilizadores.Update(userDesbloquear);
                    await _context.SaveChangesAsync();

                    //ENVIAR EMAIL A AVISAR SOBRE O DESBLOQEIO
                    //var url = "http://localhost:10382/Utilizadores/Login";
                    //await _mailservice.sendEmailAsync(userDesbloquear.Email, "Suspensão Cancelada",
                    //     "<h1>Fim da suspensão</h1>" + $"<p> A sua suspensão foi cancelada por um dos nossos admins, já está autorizado a utilizar o nosso site novamente. Faça login a partir do seguinte <a href='{url}'>link</a></p>");

                    return new Response
                    {
                        IsSuccess = true,
                        Message = "Cliente desbloquado com sucesso!"
                    };

                }
                catch
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Erro ao suspender o cliente."
                    };
                }
        }

        public async Task<Response> SuspenderUtilizadorAsync(SuspenderUserDto suspenderDto)
        {
            if (suspenderDto == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Erro ao suspender o cliente."
                };
            }

            var userSuspender = _context.Utilizadores.FirstOrDefault(x => x.Id == suspenderDto.IdUser);

            try
            {
                Suspensões novaSuspensao = new Suspensões()
                {
                    IdAdm = suspenderDto.IdAdmin,
                    IdU = suspenderDto.IdUser,
                    Motivo = suspenderDto.Motivo,
                    DataBloqueio = DateTime.Now.AddDays(suspenderDto.Dias)
                };

                userSuspender.Suspenso = true;

                _context.Utilizadores.Update(userSuspender);
                _context.Suspensões.Add(novaSuspensao);
                await _context.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = "Cliente suspenso com sucesso!"
                };

            }
            catch
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Erro ao suspender o cliente."
                };
            }
        }

        public async Task<CantinaDto> UpdateCantinaAsync(int id, CantinaDto viewModel)
        {
            if (viewModel == null)
            {
                return new CantinaDto
                {
                    Message = "Ocorreu um erro ao atualizar a cantina",
                    IsSuccess = false
                };

            }

            try
            {
                var cantinaToUpdate = await _context.Cantinas.Include(x=>x.Localizacao).FirstOrDefaultAsync(x => x.IdCantina == id);

                if (cantinaToUpdate == null)
                {
                    return new CantinaDto
                    {
                        Message = "Cantina não existe",
                        IsSuccess = false
                    };
                }

                // Verificar se a foto foi alterada
                if (viewModel.FotoFile != null)
                {
                    cantinaToUpdate.Foto = await Images.SaveImage(viewModel.FotoFile, _env, "Cantinas");
                }

                cantinaToUpdate.Nome = viewModel.Nome;
                cantinaToUpdate.Morada = viewModel.Morada;
                cantinaToUpdate.HoraAbertura = viewModel.HoraAbertura;
                cantinaToUpdate.HoraFecho = viewModel.HoraFecho;
                cantinaToUpdate.Localizacao.Latitude = viewModel.Latitude;
                cantinaToUpdate.Localizacao.Longitude = viewModel.Longitude;

                _context.Update(cantinaToUpdate);
                await _context.SaveChangesAsync();


                var cantinaReturn = _mapper.Map<CantinaDto>(cantinaToUpdate);

                cantinaReturn.IsSuccess = true;
                cantinaReturn.Message = "Cantina alterada com sucesso!";

                return cantinaReturn;
            }
            catch (Exception e)
            {
                return new CantinaDto
                {
                    Message = "Ocorreu um erro ao atualizar a cantina",
                    IsSuccess = false
                };
            }
        }
    }
}
