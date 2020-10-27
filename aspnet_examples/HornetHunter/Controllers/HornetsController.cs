using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HornetHunter.Controllers
{
    public class HornetsController : Controller
    {
        [HttpGet]
        public IActionResult Thanks()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sighting()
        {
            return RedirectToAction("Thanks");
        }

    }
}
