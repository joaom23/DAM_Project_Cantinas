using DAM_API.Helper;
using DAM_API.Models;
using DAM_API.Models.Dtos;
using FreePasses_API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Services.Interfaces
{
    public interface IUserService
    {
        Task<Response> RegistUserAsync(RegisterDto registerDto);
        Task<Response> LoginAsync(LoginDto loginDto);
        Task<Response> ReciveEmailForgotPasswordAsync(string email);

        Task<Response> ResetPasswordAsync(ResetPasswordViewModel viewmodel);
    }
}
