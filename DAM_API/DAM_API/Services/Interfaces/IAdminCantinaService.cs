using DAM_API.Helper;
using DAM_API.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Services.Interfaces
{
    public interface IAdminCantinaService
    {
        Task<Response> AdicionarPratosAsync(PratosDto pratoDto);
        Task<ReturnPratosDto> GetTodosPratosAsync();
        Task<PratosDto> GetPratoAsync(int Id);
        Task<PratosDto> UpdatePratoAsync(int Id, PratosDto pratoDto);
        Task<Response> AssociarPratoDiaAsync(PratoDiaDto pratoDiaDto);
        Task<PratoDiaDto> GetPratoDiaAsync(int IdCantina, DateTime data);
        Task<Response> ValidarQRCodeAsync(ReservaDto reservaDto);
    }
}
