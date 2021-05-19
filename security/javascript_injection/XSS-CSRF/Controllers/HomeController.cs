using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using XSS_CSRF.Models;

namespace XSS_CSRF.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Capitalize(string input)
        {
            if(String.IsNullOrEmpty(input) || input.Length < 2)
            {
                return View();
            }
            return View("Capitalize", CapitalizeFirst(input));
        }

        [NonAction]
        public static string CapitalizeFirst(string input)
        {
            string capitalized = char.ToUpper(input[0]) + input.Substring(1);
            return capitalized;
        }

        [HttpGet]
        public IActionResult WithdrawFunds()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult WithdrawFunds(int? amount, string accountNumber)
        {
            ViewBag.Message = "Withdrawl successful!";
            return RedirectToAction("Index","Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
