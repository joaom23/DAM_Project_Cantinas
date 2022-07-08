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
    public class CustomerController : ControllerBase
    {
        private ICustomerService _costumerService;
        private UserManager<Utilizador> _userManager;
        private readonly IConfiguration _configuration;

        public CustomerController(ICustomerService costumerService, UserManager<Utilizador> userManager, IConfiguration configuration)
        {
            _costumerService = costumerService;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet("Cantinas")]
        public async Task<IActionResult> GetCantinasAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _costumerService.GetCantinasAsync();

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpPost("MarcarRefeicao")]
        public async Task<IActionResult> MarcarRefeicaoAsync([FromForm] ReservaDto reserva)
        {
            if (ModelState.IsValid)
            {
                var result = await _costumerService.MarcarRefeicaoAsync(reserva);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpPost("AnularRefeicao")]
        public async Task<IActionResult> AnularRefeicaoAsync([FromForm] ReservaDto reserva)
        {
            if (ModelState.IsValid)
            {
                var result = await _costumerService.AnularRefeicaoAsync(reserva);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpPost("VerificarReserva")]
        public async Task<IActionResult> VerificarReservaAsync([FromForm] VerificarReservaDto verificar)
        {
            if (ModelState.IsValid)
            {
                var result = await _costumerService.VerificarReservaAsync(verificar);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpGet("QRCodes/{IdCustomer}")]
        public async Task<IActionResult> GetQRCodesAsync(string IdCustomer)
        {
            if (ModelState.IsValid)
            {
                var result = await _costumerService.ReceberQRCodesAsync(IdCustomer);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Algumas propriedades não são válidas");
        }

        [HttpGet("LocalizacaoCantinas")]
        public async Task<IActionResult> GetLocalizacaoCantinasAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _costumerService.GetLocalizacaoCantinasAsync();

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
