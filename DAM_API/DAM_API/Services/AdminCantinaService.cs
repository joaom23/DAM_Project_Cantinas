using AutoMapper;
using DAM_API.Data;
using DAM_API.Helper;
using DAM_API.Models;
using DAM_API.Models.Dtos;
using DAM_API.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Services
{
    public class AdminCantinaService : IAdminCantinaService
    {
        private readonly UserManager<Utilizador> _userManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AdminCantinaService(UserManager<Utilizador> userManager, AppDbContext context, IConfiguration configuration, IMapper mapper, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
            _env = env;
        }

        // Adicionar prato
        public async Task<Response> AdicionarPratosAsync(PratosDto viewModel)
        {
            if (viewModel == null)
            {
                return new Response
                {
                    Message = "Ocorreu um erro ao adicionar o prato",
                    IsSuccess = false
                };

            }

            try
            {
                //Passa os dados do viewModel para o model que vai ser adicionado na base de dados
                Prato p = _mapper.Map<Prato>(viewModel);

                // Atualiza o nome da foto e guarda-a a mesma foto
                p.Foto = await Images.SaveImage(p.FotoFile, _env, "Pratos"); 

                _context.Pratos.Add(p);
                await _context.SaveChangesAsync();

                return new Response
                {
                    Message = "Prato adicionado com sucesso!",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new Response
            {
                Message = "Ocorreu um erro ao adicionar o prato",
                IsSuccess = false
            };
        }

        public async Task<Response> AssociarPratoDiaAsync(PratoDiaDto pratoDiaDto)
        {
            if (pratoDiaDto == null)
            {
                return new Response
                {
                    Message = "Ocorreu um erro ao adicionar o prato do dia",
                    IsSuccess = false
                };

            }

            //var cantina = _context.Cantinas.FirstOrDefaultAsync(x=>x.IdCantina == pratoDiaDto.CantinaId);

            //var prato = _context.Pratos.FirstOrDefault(x => x.IdPrato == pratoDiaDto.PratoId);
            try
            {
                var pratoDiaExsite = await _context.PratosDia.FirstOrDefaultAsync(x=>x.CantinaId == x.CantinaId && x.Data == pratoDiaDto.Data);

            

                if(pratoDiaExsite == null)
                {
                    PratoDia pratoDia = _mapper.Map<PratoDia>(pratoDiaDto);
                    _context.PratosDia.Add(pratoDia);
                }
                else
                {
                    pratoDiaExsite.PratoId = pratoDiaDto.PratoId;
                    _context.PratosDia.Update(pratoDiaExsite);
                }
            
            await _context.SaveChangesAsync();

                return new Response
                {
                    Message = "Prato do dia adiocionado!",
                    IsSuccess = true
                };

            }
            catch (Exception)
            {
                return new Response
                {
                    Message = "Ocorreu um erro ao adicionar o prato do dia",
                    IsSuccess = false
                };
            }

        }

        public async Task<PratosDto> GetPratoAsync(int Id)
        {
                var prato = await _context.Pratos.FirstOrDefaultAsync(x=> x.IdPrato == Id);
                
                if(prato != null)
                {
               var pratoReturn = _mapper.Map<PratosDto>(prato);
                pratoReturn.IsSuccess = true;

                return pratoReturn;
                }

            return new PratosDto 
            {
                IsSuccess = false,
                Message = "Erro ao retornar o prato."
            };
        }

        public async Task<PratoDiaDto> GetPratoDiaAsync(int IdCantina, DateTime data)
        {
            if (IdCantina == 0 || data == null)
            {
                return new PratoDiaDto
                {
                    Message = "Ocorreu um erro ao receber o prato do dia",
                    IsSuccess = false
                };
            }

            var pratoDia = await _context.PratosDia.Include(x=>x.Prato).Include(x=>x.Cantina).FirstOrDefaultAsync(x=> x.CantinaId == IdCantina && x.Data == data);

            if (pratoDia != null)
            {
                PratoDiaDto returnPratoDia = _mapper.Map<PratoDiaDto>(pratoDia);
                returnPratoDia.IsSuccess = true;

                return returnPratoDia;
            }

            var Cantina = await _context.Cantinas.FirstOrDefaultAsync(x=>x.IdCantina == IdCantina);

            return new PratoDiaDto
            {
                Cantina = Cantina,
                IsSuccess = true,
                Message = $"Não exisite prato defenido para a data {data}"
            };
        }

        public async Task<ReturnPratosDto> GetTodosPratosAsync()
        {
            ReturnPratosDto pratosList = new ReturnPratosDto();
            try
            {

                pratosList.Pratos = await _context.Pratos.ToListAsync();

                if (pratosList.Pratos != null)
                {
                    pratosList.IsSuccess = true;
                    return pratosList;
                }

                pratosList.IsSuccess = true;
                pratosList.Message = "Não existem pratos disponiveis.";
                return pratosList;

            }catch(Exception e)
            {
                pratosList.IsSuccess = false;
                pratosList.Message = "Ocorreu um erro ao receber os pratos";
                return pratosList;
            }

        }

        public async Task<PratosDto> UpdatePratoAsync(int id, PratosDto viewModel)
        {
            if (viewModel == null)
            {
                return new PratosDto
                {
                    Message = "Ocorreu um erro ao atualizar o prato",
                    IsSuccess = false
                };

            }

            try
            {
                var pratoToUpdate = await _context.Pratos.FirstOrDefaultAsync(x => x.IdPrato == id);

                if(pratoToUpdate == null)
                {
                    return new PratosDto
                    {
                        Message = "Prato não existe",
                        IsSuccess = false
                    };
                }

                // Verificar se a foto foi alterada
                if(viewModel.FotoFile != null)
                {
                    pratoToUpdate.Foto = await Images.SaveImage(viewModel.FotoFile, _env, "Pratos");
                }

                pratoToUpdate.Descricao = viewModel.Descricao;
                pratoToUpdate.Preco = viewModel.Preco;

                _context.Update(pratoToUpdate);
                await _context.SaveChangesAsync();


                var pratoReturn = _mapper.Map<PratosDto>(pratoToUpdate);

                pratoReturn.IsSuccess = true;
                pratoReturn.Message = "Prato alterado com sucesso!";

                return pratoReturn;
                
            }
            catch (Exception)
            {
                return new PratosDto
                {
                    Message = "Ocorreu um erro ao atualizar o prato",
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> ValidarQRCodeAsync(ReservaDto reserva)
        {
            try
            {
                // Divide a informaçao do qr code, primeiro é o id do prato, depois id do user depois a data
                var QRCodeInformation = reserva.QRCode.Split("_");
                var IdPratoDia = Convert.ToInt32(QRCodeInformation[0]);
                var IdCustomer = QRCodeInformation[1];
                var Data = QRCodeInformation[2];

                //var Hoje = DateTime.Now;
                //var diaHoje = Hoje.Day;
                //var mesHoje = Hoje.Month;
                //var anoHoje = Hoje.Year;

                //var dataHoje = anoHoje + "-" + mesHoje + "-" + diaHoje;

                var DataEmDateTime = Convert.ToDateTime(Data);
                var hoje = DateTime.Now.Date;

                if(DataEmDateTime < hoje)
                {
                    // QR invalido (antigo)
                }

                if(IdPratoDia != reserva.PratoDiaId)
                {
                    // QR invalido (provavelmenmte qr code de outra cantina porque o id do prato do dia é diferente)
                }

                // Verificar se qr code existe mesmo
                var existeQRCode = await _context.Reservas.FirstOrDefaultAsync(x=> x.QRCode == reserva.QRCode);

                if(existeQRCode != null)
                {
                    if (existeQRCode.FoiLido)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "QR Code já foi lido."
                        };
                    }

                    existeQRCode.FoiLido = true;
                    _context.Update(existeQRCode);

                    var atualizarPratoDia = await _context.PratosDia.FirstOrDefaultAsync(x=>x.IdPratoDia == IdPratoDia);
                    atualizarPratoDia.RefeicoesConsumidas++;
                    _context.Update(atualizarPratoDia);

                    await _context.SaveChangesAsync();

                    return new Response
                    {
                        Update = atualizarPratoDia.RefeicoesConsumidas,
                        IsSuccess = true,
                        Message = "QR Code validado com sucesso!"
                    };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = "QR Code inválido."
                };
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return new Response
            {
                IsSuccess = false,
                Message = "Ocorreu um erro ao validar o QR Code."
            };
        }
    }
}
