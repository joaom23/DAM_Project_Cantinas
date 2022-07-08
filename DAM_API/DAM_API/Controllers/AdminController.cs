using DAM_API.Models;
using DAM_API.Models.Dtos;
using DAM_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;
        private UserManager<Utilizador> _userManager;
        private readonly IConfiguration _configuration;

        public AdminController(IAdminService adminService, UserManager<Utilizador> userManager, IConfiguration configuration)
        {
            _adminService = adminService;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("CriarCantina")]
        public async Task<IActionResult> CriarCantinaAsync([FromForm] CantinaDto viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.CriarCantinaAsync(viewModel);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpGet("GetUtilizadores")]
        public async Task<IActionResult> GetUtilizadoresAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.GetUtilizadoresAsync();

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpPost("Suspender")]
        public async Task<IActionResult> SuspenderUtilizadorAsync([FromForm] SuspenderUserDto suspenderDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.SuspenderUtilizadorAsync(suspenderDto);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpPost("RemoverSuspensao")]
        public async Task<IActionResult> RemoverSuspensaoAsync([FromForm] string userId)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.RemoverSuspensaoAsync(userId);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpPut]
        [Route("Cantina/{id}")]
        public async Task<IActionResult> UpdateCantinaAsync(int id, [FromForm] CantinaDto cantina)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.UpdateCantinaAsync(id, cantina);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }
    }
}
