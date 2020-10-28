using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HornetHunter.Models;
using Microsoft.AspNetCore.Mvc;

namespace HornetHunter.Controllers
{
    public class HornetsController : Controller
    {
        private HornetContext db;

        public HornetsController(HornetContext context)
        {
            this.db = context;
            Debug.WriteLine(db.Sightings.FirstOrDefault());
        }

        [HttpGet]
        public IActionResult Thanks()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sighting(Sighting sighting)
        {
            Debug.WriteLine(sighting);
            if(ModelState.IsValid)
            {
                sighting.ReportTime = DateTime.UtcNow;
                sighting.Ipaddress = HttpContext.Connection.RemoteIpAddress.ToString();
                Debug.WriteLine(sighting);

                db.Sightings.Add(sighting);
                db.SaveChanges();
                return RedirectToAction("Thanks");
            }
            return View("Views/Home/Index",sighting);
            
        }

        [HttpGet]
        public IActionResult ShowSightings()
        {
            IEnumerable<Sighting> all = db.Sightings.OrderByDescending(s => s.SightingTime).ToList();
            return View(all);
        }

        public IActionResult Map()
        {
            IEnumerable<Sighting> all = db.Sightings.ToList();
            return View(all);
        }
    }
}
