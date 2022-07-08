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
using System.Text;
using System.Threading.Tasks;

namespace DAM_BackOffice.Controllers
{
    public class AdminCantinaController : Controller
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;

        public AdminCantinaController(IMapper mapper)
        {
            _client = new HttpClient();
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Pratos()
        {
            var response = await _client.GetAsync(APIServer.Ip + "/api/admincantina/pratos");

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }

            
            var pratosList = JsonConvert.DeserializeObject<ReturnPratosDto>(responsebody);
            ListaPratos.Pratos = pratosList.Pratos;

            return View(pratosList);
        }

        [HttpGet]
        public IActionResult AdicionarPrato(string Type, string Message)
        {
            ViewBag.Type = Type;
            ViewBag.Message = Message;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPrato(IFormCollection dados)
        {
            if (ModelState.IsValid)
            {
                var formContent = new MultipartFormDataContent();

                formContent.Add(new StringContent(dados["Descricao"].ToString()), "Descricao");
                formContent.Add(new StringContent(dados["Preco"].ToString()), "Preco");
                formContent.Add(new StreamContent(dados.Files["FotoFile"].OpenReadStream()), "FotoFile", Path.GetFileName(dados.Files["FotoFile"].FileName));

                var response = await _client.PostAsync(APIServer.Ip + "/api/admincantina/adicionarprato", formContent);

                var responsebody = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return View("SemPermissoes");
                }

                var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

                if (responseObject.IsSuccess && responseObject != null)
                {
                    return RedirectToAction("Index", "Home", new { Type = "success", Message = responseObject.Message });
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { Type = "danger", Message = responseObject.Message });
                }
            }
            return View();
        }

        public IActionResult EditarPrato(string Type, string Message, int IdPrato)
        {
            ViewBag.Type = Type;
            ViewBag.Message = Message;

            var prato = ListaPratos.Pratos.Find(x => x.IdPrato == IdPrato);

            return View(prato);
        }

        [HttpPost]
        public async Task<IActionResult> EditarPrato(IFormCollection dados)
        {
            if (ModelState.IsValid)
            {
                var IdPrato = dados["IdPrato"];
                var formContent = new MultipartFormDataContent();

                formContent.Add(new StringContent(dados["Descricao"].ToString()), "Descricao");
                formContent.Add(new StringContent(dados["Preco"].ToString()), "Preco");

                if (dados.Files.Count > 0)
                {
                    formContent.Add(new StreamContent(dados.Files["FotoFile"].OpenReadStream()), "FotoFile", Path.GetFileName(dados.Files["FotoFile"].FileName));
                }

                var response = await _client.PutAsync(APIServer.Ip + "/api/admincantina/prato/" + IdPrato, formContent);

                var responsebody = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return View("SemPermissoes");
                }

                var responseObject = JsonConvert.DeserializeObject<PratosDto>(responsebody);

                if (responseObject.IsSuccess && responseObject != null)
                {
                    Prato p = _mapper.Map<Prato>(responseObject);
                    ListaPratos.UpdateListaPratos(p);                   
                    return RedirectToAction("EditarPrato", "AdminCantina", new { Type = "success", Message = responseObject.Message, IdPrato = IdPrato });
                }
                else
                {
                    return RedirectToAction("EditarPrato", "AdminCantina", new { Type = "success", Message = responseObject.Message, IdPrato = IdPrato });
                }
            }
            return View();
        }

        [HttpGet]
        public  async Task<IActionResult> PratosDia(int IdCantina, string Data, string Type, string Message)
        {

            ViewBag.Type = Type;
            ViewBag.Message = Message;

            ViewBag.Data = Data;
            ViewBag.CantinaId = IdCantina;

            if (Data != null)
            {
                var response = await _client.GetAsync(APIServer.Ip + "/api/admincantina/pratodia/" + IdCantina + "/" + Data);

                var responsebody = await response.Content.ReadAsStringAsync();

                var pratoDia = JsonConvert.DeserializeObject<PratoDiaDto>(responsebody);

                if (pratoDia.Prato == null)
                {
                    //if (Count.count == 0)
                    //{
                    //    Count.count++;
                    //    return RedirectToAction("PratosDia", "AdminCantina", new { Type = "danger", Message = pratoDia.Message, IdCantina = IdCantina, Data = data });
                    //}

                    //Count.count = 0;

                    ViewBag.Type = "danger";
                    ViewBag.Message = pratoDia.Message;

                    return View(pratoDia);
                }

                return View(pratoDia);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Cantinas()
        {
            if(ListaPratos.Pratos == null)
            {
                await Pratos();
            }

            var response = await _client.GetAsync(APIServer.Ip + "/api/customer/cantinas");

            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return View("SemPermissoes");
            }


            var cantinasList = JsonConvert.DeserializeObject<ReturnCantinasDto>(responsebody);
            ListaCantinas.Cantinas = cantinasList.Cantinas;

            return View(cantinasList);
        }

        [HttpGet]
        public IActionResult AssociarPratoDia(int IdCantina, string Data, string Type, string Message)
        {
            ViewBag.Type = Type;
            ViewBag.Message = Message;

            ViewBag.data = Data;
            ViewBag.IdCantina = IdCantina;

            ReturnPratosDto pratos = new ReturnPratosDto();
            pratos.Pratos = ListaPratos.Pratos;

            return View(pratos);
        }

        [HttpPost]
        public async Task<IActionResult> AssociarPratoDia(int IdCantina, int IdPrato, string Data)
        {
            PratoDiaDto prato = new PratoDiaDto
            {
                PratoId = IdPrato,
                Data = Convert.ToDateTime(Data),
                CantinaId = IdCantina
            };

            var data = new StringContent(
                   JsonConvert.SerializeObject(prato, Formatting.Indented),
                    Encoding.UTF8,
                    "application/json");

            var response = await _client.PostAsync(APIServer.Ip + "/api/admincantina/pratodia", data);

            var responsebody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

            return RedirectToAction("PratosDia", new { IdCantina = IdCantina, Data = Data, Type = "success", Message = responseObject.Message });
        }
    }
   
}
