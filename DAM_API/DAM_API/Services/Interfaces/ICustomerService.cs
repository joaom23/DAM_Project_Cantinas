using DAM_API.Helper;
using DAM_API.Models;
using DAM_API.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<ReturnCantinasDto> GetCantinasAsync();
        Task<Response> MarcarRefeicaoAsync(ReservaDto reservaDto);
        Task<VerificarReservaDto> VerificarReservaAsync(VerificarReservaDto verificarDto);
        Task<Response> AnularRefeicaoAsync(ReservaDto anularReservaDto);
        Task<ReturnQRCodesDto> ReceberQRCodesAsync(string IdCustomer);
        Task<PositionCantinasDto> GetLocalizacaoCantinasAsync();
    }
}
