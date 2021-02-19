using AsyncExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncExample.Controllers
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

        public IActionResult GetDataSynchronous()
        {
            _logger.LogInformation("GetDataSynchronous");
            EarthquakeAPI quakeSource = new EarthquakeAPI("https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/2.5_day.geojson");
            IEnumerable<Earthquake> quakes  = quakeSource.GetRecentEarthquakes().OrderByDescending(e => e.Magnitude);
            BitcoinPriceIndex bpi           = CoinDeskAPI.GetBPI();
            return Json(new { earthquakes = quakes, bitcoinPrices = bpi });
        }

        public async Task<IActionResult> GetDataAsynchronous()
        {
            _logger.LogInformation("GetDataAsynchronous");
            EarthquakeAPI quakeSource = new EarthquakeAPI("https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/2.5_day.geojson");
            IEnumerable<Earthquake> quakes  = await quakeSource.GetRecentEarthquakesAsync();
            BitcoinPriceIndex bpi           = await CoinDeskAPI.GetBPIAsync();
            return Json(new { earthquakes = quakes.OrderByDescending(e => e.Magnitude), bitcoinPrices = bpi });
        }

        public async Task<IActionResult> GetDataAsynchronousParallel()
        {
            _logger.LogInformation("GetDataAsynchronousParallel");
            EarthquakeAPI quakeSource = new EarthquakeAPI("https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/2.5_day.geojson");
            // Start both asynchronous requests, each one returns a Task object immediately
            var task1 = quakeSource.GetRecentEarthquakesAsync();
            var task2 = CoinDeskAPI.GetBPIAsync();
            // await both of them
            await Task.WhenAll(task1, task2);
            // "Lift" results out of each task
            IEnumerable<Earthquake> quakes  = task1.Result;
            BitcoinPriceIndex bpi           = task2.Result;
            return Json(new { earthquakes = quakes.OrderByDescending(e => e.Magnitude), bitcoinPrices = bpi });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
