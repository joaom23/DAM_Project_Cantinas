using AutoMapper;
using DAM_API.Data;
using DAM_API.Helper;
using DAM_API.Models;
using DAM_API.Models.Dtos;
using DAM_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly UserManager<Utilizador> _userManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CustomerService(UserManager<Utilizador> userManager, AppDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<Response> AnularRefeicaoAsync(ReservaDto anularReservaDto)
        {
            try
            {
                var refeicao = await _context.Reservas.FirstOrDefaultAsync(x=>x.CustomerId == anularReservaDto.CustomerId && x.PratoDiaId == anularReservaDto.PratoDiaId);
                _context.Reservas.Remove(refeicao);

                var atualizarReservas = await _context.PratosDia.FirstOrDefaultAsync(x => x.IdPratoDia == anularReservaDto.PratoDiaId);
                atualizarReservas.RefeicoesMarcadas--;
                _context.PratosDia.Update(atualizarReservas);

                await _context.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = "Refeição anulada com sucesso!"
                };
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return new Response
            {
                IsSuccess = false
            };
        }

        public async Task<ReturnCantinasDto> GetCantinasAsync()
        {
            ReturnCantinasDto cantinasList = new ReturnCantinasDto();
            try { 

                cantinasList.Cantinas = await _context.Cantinas.Include(x=> x.Localizacao).Include(x=>x.PratosDia).ToListAsync();
           
            if (cantinasList.Cantinas != null)
            {
                cantinasList.IsSuccess = true;
                return cantinasList;
            }

                cantinasList.IsSuccess = true;
                cantinasList.Message = "Não existem cantinas disponíveis";
                return cantinasList;
            }
            catch(Exception e)
            {
                cantinasList.IsSuccess = false;
                cantinasList.Message = "Ocorreu um erro ao receber as cantinas";

                return cantinasList;
            }
        }

        public async Task<PositionCantinasDto> GetLocalizacaoCantinasAsync()
        {
            var cantinas = await _context.Cantinas.Include(x=>x.Localizacao).ToListAsync();

            if(cantinas.Count == 0)
            {
                return new PositionCantinasDto
                {
                    IsSuccess = false,
                    PosicaoCantinas = null
                };
            }

            var posicaoCantinas = new PositionCantinasDto();
            posicaoCantinas.PosicaoCantinas = new List<PositionCantina>();

            foreach (var item in cantinas)
            {
                var auxCantina = new PositionCantina
                {
                    cantinaId = item.IdCantina,
                    Latitude = item.Localizacao.Latitude,
                    Longitude = item.Localizacao.Longitude,
                    Nome = item.Nome
                };

                posicaoCantinas.PosicaoCantinas.Add(auxCantina);
            }

            posicaoCantinas.IsSuccess = true;
            return posicaoCantinas;
        }

        public async Task<Response> MarcarRefeicaoAsync(ReservaDto reservaDto)
        {
            try
            {
                reservaDto.FoiLido = false;
                var reserva = _mapper.Map<Reserva>(reservaDto);

                _context.Reservas.Add(reserva);
                

                var atualizarReservas = await _context.PratosDia.FirstOrDefaultAsync(x=>x.IdPratoDia == reservaDto.PratoDiaId);
                atualizarReservas.RefeicoesMarcadas++;
                _context.PratosDia.Update(atualizarReservas);

                await _context.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = "Reserva efetuada com sucesso!",
                };
            }
            catch(Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Erro ao efetuar a reserva"
                };
            }
        }

        public async Task<ReturnQRCodesDto> ReceberQRCodesAsync(string IdCustomer)
        {
            List<QRCodeInformationDto> QRCodes = new List<QRCodeInformationDto>();

            try
            {              
                var qrCodesInformation = await _context.Reservas.Where(x=>x.CustomerId == IdCustomer).Include(x=>x.PratoDia.Cantina).ToListAsync();

                foreach (var item in qrCodesInformation)
                {
                    var newQRCode = new QRCodeInformationDto
                    {
                        NomeCantina = item.PratoDia.Cantina.Nome,
                        Data = item.PratoDia.Data.ToShortDateString(),
                        QRCode = item.QRCode
                    };

                    QRCodes.Add(newQRCode);
                }

                return new ReturnQRCodesDto
                {
                    QRCodesInformation = QRCodes,
                    IsSuccess = true
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return new ReturnQRCodesDto
            {
                QRCodesInformation = new List<QRCodeInformationDto>(),
                IsSuccess = false
            };
        }

        public async Task<VerificarReservaDto> VerificarReservaAsync(VerificarReservaDto verificarDto)
        {
            try
            {
                var temReserva = await _context.Reservas.Where(x=>x.CustomerId == verificarDto.ClienteId && x.PratoDiaId == verificarDto.PratoDiaId).ToListAsync();

                if(temReserva.Count == 0)
                {
                    return new VerificarReservaDto
                    {
                        IsSuccess = true,
                        TemReserva = false
                    };
                }
                    return new VerificarReservaDto
                    {
                        IsSuccess = true,
                        TemReserva = true
                    };
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return new VerificarReservaDto
            {
                IsSuccess = false
            };
        }
    }
}
