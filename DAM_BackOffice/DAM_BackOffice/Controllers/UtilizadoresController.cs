using DAM_BackOffice.Helper;
using DAM_BackOffice.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAM_BackOffice.Controllers
{
    public class UtilizadoresController : Controller
    {
        private readonly HttpClient _client;

        public UtilizadoresController(IConfiguration configuration)
        {
            _client = new HttpClient();
        }

        [HttpGet]
        public IActionResult Register(string Type, string Message)
        {

            ViewBag.Type = Type;
            ViewBag.Message = Message;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(IFormCollection dados)
        {
            if (ModelState.IsValid)
            {
                RegisterDto user = new RegisterDto
                {
                    Email = dados["Email"],
                    Password = dados["Password"],
                    ConfirmPassword = dados["ConfirmPassword"],
                    Role = "Cliente"
                };

                var formContent = new MultipartFormDataContent
                {
                     { new StringContent(user.Email), "Email" },
                     { new StringContent(user.Password), "Password" },
                     { new StringContent(user.ConfirmPassword), "ConfirmPassword" },
                    {new StringContent(user.Role), "Role" }

                };

                var response = await _client.PostAsync(APIServer.Ip + "/api/users/RegisterUser", formContent);

                var responsebody = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

                if (responseObject.IsSuccess)
                {
                    return RedirectToAction("Index", "Home", new { Type = "success", Message = responseObject.Message });
                }
                else
                {
                    return RedirectToAction("Register", "Utilizadores", new { Type = "danger", Message = responseObject.Message });
                }

            }
            return View();
        }

        [HttpGet]
        public IActionResult Login(string Type, string Message)
        {
            ViewBag.Type = Type;
            ViewBag.Message = Message;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(IFormCollection dados)
        {
            if (ModelState.IsValid)
            {
                LoginDto user = new LoginDto
                {
                    Email = dados["Email"],
                    Password = dados["Password"]
                };

                var formContent = new MultipartFormDataContent
                {
                     { new StringContent(user.Email), "Email" },
                     { new StringContent(user.Password), "Password" }
                };

                var response = await _client.PostAsync(APIServer.Ip + "/api/users/login", formContent);

                var responsebody = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

                if (responseObject.IsSuccess)
                {
                    var token = responseObject.Message;

                    var readToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

                    var userId = readToken.Claims.FirstOrDefault(x => x.Type == "Id").Value;
                    var userEmail = readToken.Claims.FirstOrDefault(x => x.Type == "Email").Value;
                    var userRole = readToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

                    if (userRole == "Cliente")
                    {
                        // Vai buscar a foto do user
                        //var ImagemUser = readToken.Claims.FirstOrDefault(x => x.Type == "Foto").Value;
                        //HttpContext.Session.SetString("Foto", ImagemUser);
                    }
                    else if (userRole == "Admin")
                    {
                        var nomeAdmin = readToken.Claims.FirstOrDefault(x => x.Type == "Nome").Value;
                        HttpContext.Session.SetString("NomeAdmin", nomeAdmin);
                    }

                    HttpContext.Session.SetString("Token", token.ToString());
                    HttpContext.Session.SetString("Id", userId);
                    HttpContext.Session.SetString("Role", userRole);
                    HttpContext.Session.SetString("Email", userEmail);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", new { Type = "danger", Message = responseObject.Message });
                }

            }

            return View();

        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("CookieDeSessao");
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Home");
            
        }

        public static bool Autenticado(HttpContext context)
        {
            if (context.Session.GetInt32("Token") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Administrador(HttpContext context)
        {
            if (context.Session.GetString("Role") == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Cliente(HttpContext context)
        {
            if (context.Session.GetString("Role") == "Cliente")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
