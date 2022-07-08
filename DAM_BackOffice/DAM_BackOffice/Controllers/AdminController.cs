using AutoMapper;
using DAM_BackOffice.Helper;
using DAM_BackOffice.Models;
using DAM_BackOffice.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DAM_BackOffice.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;

        public AdminController(IMapper mapper)
        {
            _client = new HttpClient();
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegistAdminCantina(string Type, string Message)
        {

            ViewBag.Type = Type;
            ViewBag.Message = Message;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistAdminCantina(IFormCollection dados)
        {
            if (ModelState.IsValid)
            {
                RegisterDto user = new RegisterDto
                {
                    Email = dados["Email"],
                    Password = dados["Password"],
                    ConfirmPassword = dados["ConfirmPassword"],
                    Role = "AdminCantina"
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
        public async Task<IActionResult> ManageUsers(string Type, string Message)
        {
            ViewBag.Type = Type;
            ViewBag.Message = Message;

            var response = await _client.GetAsync(APIServer.Ip + "/api/admin/getutilizadores");

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }

            var users = JsonConvert.DeserializeObject<ReturnUtilizadoresDto>(responsebody);

            List<Utilizador> utilizadores = new List<Utilizador>();

            foreach (var u in users.Utilizadores)
            {
                utilizadores.Add(u);
            }

            return View(utilizadores);
        }

        [HttpPost]
        public async Task<IActionResult> SuspenderCliente(IFormCollection dados)
        {
            var adminId = HttpContext.Session.GetString("Id");

            var userId = dados["userId"];
            var motivo = dados["Motivo"];
            var dias = dados["Dias"];

            var formContent = new MultipartFormDataContent
                {
                     { new StringContent(userId), "IdUser" },
                     { new StringContent(adminId), "IdAdmin" },
                     { new StringContent(motivo), "Motivo" },
                     { new StringContent(dias.ToString()), "Dias" }
            };

            var response = await _client.PostAsync(APIServer.Ip + "/api/admin/suspender/", formContent);

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }

            var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

            if (responseObject.IsSuccess)
            {
                return RedirectToAction("ManageUsers", new { Type = "success", Message = responseObject.Message });
            }
            else
            {
                return RedirectToAction("ManageUsers", new { Type = "danger", Message = responseObject.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> RemoverSuspensaoCliente(string userId)
        {
            var formContent = new MultipartFormDataContent
                {
                     { new StringContent(userId), "userId" },
            };

            var response = await _client.PostAsync(APIServer.Ip + "/api/admin/removersuspensao/", formContent);

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }

            var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

            if (responseObject.IsSuccess)
            {
                return RedirectToAction("ManageUsers", new { Type = "success", Message = responseObject.Message });
            }
            else
            {
                return RedirectToAction("ManageUsers", new { Type = "danger", Message = responseObject.Message });
            }
        }

        public IActionResult CriarCantinas(string Message, string Type)
        {
            ViewBag.Message = Message;
            ViewBag.Type = Type;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarCantinas(IFormCollection dados)
        {
            if (ModelState.IsValid)
            {
                CantinaDto cantina = new CantinaDto
                {
                    Nome = dados["Nome"],
                    Morada = dados["Morada"],
                    HoraAbertura = TimeSpan.Parse(dados["HoraAbertura"]),
                    HoraFecho = TimeSpan.Parse(dados["HoraFecho"]),
                    Latitude = Convert.ToDouble(dados["Latitude"]),
                    Longitude = Convert.ToDouble(dados["Longitude"])
                };

                var formContent = new MultipartFormDataContent
                {
                     { new StringContent(cantina.Morada), "Morada" },
                     { new StringContent(cantina.Nome), "Nome" },
                     { new StringContent(cantina.HoraAbertura.ToString()), "HoraAbertura" },
                     { new StringContent(cantina.HoraFecho.ToString()), "HoraFecho" },
                     { new StringContent(cantina.Latitude.ToString()), "Latitude" },
                     { new StringContent(cantina.Longitude.ToString()), "Longitude" },
                     { new StreamContent(dados.Files["FotoFile"].OpenReadStream()), "FotoFile", Path.GetFileName(dados.Files["FotoFile"].FileName) }
            };

                var response = await _client.PostAsync(APIServer.Ip + "/api/admin/criarcantina", formContent);

                var responsebody = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

                if (responseObject.IsSuccess)
                {
                    return RedirectToAction("Index", "Home", new { Type = "success", Message = responseObject.Message });
                }
                else
                {
                    return RedirectToAction("CriarCantinas", "Admin", new { Type = "danger", Message = responseObject.Message });
                }

            }
            return View();
        }

        public IActionResult EditarCantinas(string Type, string Message, int IdCantina)
        {
            ViewBag.Type = Type;
            ViewBag.Message = Message;

            var cantina = ListaCantinas.Cantinas.Find(x => x.IdCantina == IdCantina);

            return View(cantina);
        }

        [HttpPost]
        public async Task <IActionResult> EditarCantinas(IFormCollection dados)
        {
            if (ModelState.IsValid)
            {
                var IdCantina = dados["IdCantina"];
                var formContent = new MultipartFormDataContent();

                formContent.Add(new StringContent(dados["Nome"].ToString()), "Nome");
                formContent.Add(new StringContent(dados["Morada"].ToString()), "Morada");
                formContent.Add(new StringContent(dados["HoraAbertura"].ToString()), "HoraAbertura");
                formContent.Add(new StringContent(dados["HoraFecho"].ToString()), "HoraFecho");
                formContent.Add(new StringContent(dados["Localizacao.Latitude"].ToString()), "Latitude");
                formContent.Add(new StringContent(dados["Localizacao.Longitude"].ToString()), "Longitude");


                if (dados.Files.Count > 0)
                {
                    formContent.Add(new StreamContent(dados.Files["FotoFile"].OpenReadStream()), "FotoFile", Path.GetFileName(dados.Files["FotoFile"].FileName));
                }

                var response = await _client.PutAsync(APIServer.Ip + "/api/admin/cantina/" + IdCantina, formContent);

                var responsebody = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return View("SemPermissoes");
                }

                var responseObject = JsonConvert.DeserializeObject<CantinaDto>(responsebody);

                if (responseObject.IsSuccess && responseObject != null)
                {
                    var teste2 = ListaCantinas.Cantinas;
                    Cantina c = _mapper.Map<Cantina>(responseObject);
                    ListaCantinas.UpdateListaCantinas(c);
                    var teste = ListaCantinas.Cantinas;
                    return RedirectToAction("EditarCantinas", "Admin", new { Type = "success", Message = responseObject.Message, IdCantina = IdCantina });
                }
                else
                {
                    return RedirectToAction("EditarCantinas", "Admin", new { Type = "danger", Message = responseObject.Message, IdCantina = IdCantina });
                }
            }
            return View();
        }
    }
}
