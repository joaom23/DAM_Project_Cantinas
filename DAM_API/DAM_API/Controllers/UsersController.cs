using DAM_API.Helper;
using DAM_API.Models;
using DAM_API.Models.Dtos;
using DAM_API.Services;
using DAM_API.Services.Interfaces;
using FreePasses_API.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DAM_API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private UserManager<Utilizador> _userManager;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, UserManager<Utilizador> userManager, IConfiguration configuration)
        {
            _userService = userService;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUserAsync([FromForm] RegisterDto viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegistUserAsync(viewModel);

                if (result.IsSuccess)
                {
                    //registar utilizador na base de dados

                  


                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromForm] LoginDto viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(viewModel);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpGet("ConfirmarEmail")]
        public async Task<IActionResult> ConfirmarEmailAsync(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Utilizador não encontrado"
                });
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);

            var normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
            {
                return Redirect($"{_configuration["AppUrl"]}/confirmaremail.html");
            }

            return BadRequest(new Response
            {
                IsSuccess = false,
                Message = "Erro ao confirmar o email",
            });
        }

        [HttpGet("ReciveEmailResetPassword/{email}")]
        public async Task<IActionResult> ReciveEmailResetPasswordAsync(string email)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ReciveEmailForgotPasswordAsync(email);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromForm] ResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(viewModel);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpGet]
        [Route("[action]/{folderName}/{filename}")]
        [AllowAnonymous]
        public IActionResult GetImage(string folderName, string filename)
        {
            var image = System.IO.File.OpenRead("Recursos/" + folderName + "/" + filename);

            return File(image, "image/jpeg");
        }
    }
}
