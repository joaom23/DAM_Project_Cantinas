using DAM_API.Models;
using DAM_API.Models.Dtos;
using DAM_API.Services;
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
    public class AdminCantinaController : ControllerBase
    {
        private IAdminCantinaService _adminCantinaService;
        private UserManager<Utilizador> _userManager;
        private readonly IConfiguration _configuration;

        public AdminCantinaController(IAdminCantinaService adminCantinaService, UserManager<Utilizador> userManager, IConfiguration configuration)
        {
            _adminCantinaService = adminCantinaService;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("AdicionarPrato")]
        public async Task<IActionResult> AdicionarPratoAsync([FromForm] PratosDto viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminCantinaService.AdicionarPratosAsync(viewModel);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpGet("Pratos")]
        public async Task<IActionResult> GetTodosPratosAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _adminCantinaService.GetTodosPratosAsync();

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpGet]
        [Route("Prato/{Id}")]
        public async Task<IActionResult> GetPratoAsync(int Id)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminCantinaService.GetPratoAsync(Id);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpPut]
        [Route("Prato/{id}")]
        public async Task<IActionResult> UpdatePratoAsync(int id, [FromForm]PratosDto prato)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminCantinaService.UpdatePratoAsync(id, prato);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpPost]
        [Route("PratoDia")]
        public async Task<IActionResult> AssociarPratoDiaAsync([FromBody] PratoDiaDto pratoDia)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminCantinaService.AssociarPratoDiaAsync(pratoDia);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpGet]
        [Route("PratoDia/{cantinaId}/{data}")]
        public async Task<IActionResult> GetPratoDiaAsync(int cantinaId, DateTime data)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminCantinaService.GetPratoDiaAsync(cantinaId, data);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpPost]
        [Route("ValidarQRCode")]
        public async Task<IActionResult> ValidarQRCodeAsdync([FromForm] ReservaDto reserva)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminCantinaService.ValidarQRCodeAsync(reserva);

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
