using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ex1Project.Models;

namespace Ex1Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Debug.WriteLine("Inside of Index action method.");
            return View();
        }

        [HttpGet]
        public IActionResult Search(string search)
        {
            Debug.WriteLine("Inside of Search action method.");
            Debug.WriteLine("search = " + search);
            return View("Search",search);
        }

        public IActionResult Privacy()
        {
            Debug.WriteLine("Inside of Privacy action method.");
            return View();
        }

        public string HelloWorld()
        {
            return "Hello World";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
