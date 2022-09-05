using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
        }
        public IActionResult DownloadPhysicalFile()
        {
            string filePath = Path.Combine(_appEnvironment.ContentRootPath, "wwwroot/files/json.json");
            string fileType = "application/json";
            string fileName = "json.json";
            return PhysicalFile(filePath, fileType, fileName); //скачивание физического файла из любого места
        }
        public IActionResult DownloadFile()
        {
            var filePath = Path.Combine("~/files", "json.json");
            return File(filePath, "application/json", "json.json"); //скачивание физического файла из wwwroot
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Square()
        {
            //3 перегрузки метода RedirectToAction
            return RedirectToAction("Create", "Phones", new {id=5, name="Brabras"});
        }

    }
}
