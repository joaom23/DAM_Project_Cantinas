using DAM_BackOffice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_BackOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string Type, string Message)
        {
            ViewBag.Type = Type;
            ViewBag.Message = Message;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
