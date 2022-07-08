using DAM_API.Helper;
using DAM_API.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Services.Interfaces
{
    public interface IAdminService
    {
        Task<Response> CriarCantinaAsync(CantinaDto cantinaDto);
        Task<ReturnUtilizadoresDto> GetUtilizadoresAsync();
        Task<Response> SuspenderUtilizadorAsync(SuspenderUserDto suspenderDto);
        Task<Response> RemoverSuspensaoAsync(string userId);
        Task<CantinaDto> UpdateCantinaAsync(int id, CantinaDto cantinaDto);
    }
}
