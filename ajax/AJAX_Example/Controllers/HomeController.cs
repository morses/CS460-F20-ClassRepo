using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AJAX_Example.Models;
using Microsoft.Extensions.Configuration;

namespace AJAX_Example.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger,IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult RandomNumbers(int? count = 100)
        {
            RandomNumberObject obj = new RandomNumberObject("Random Numbers API",(int)count,1000);
            return Json(obj);
        }

        [HttpPost]
        public IActionResult Earthquakes(int? hour,int? day)
        {
            Debug.WriteLine($"{hour}, {day}");
            EarthquakeAPI quakeSource = new EarthquakeAPI("https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/1.0_hour.geojson");
            IEnumerable<Earthquake> quakes = quakeSource.GetRecentEarthquakes();
            return Json(quakes);
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            string secret = _config["AXAJ1:MySecretToken"];
            Debug.WriteLine("secret " + secret);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
