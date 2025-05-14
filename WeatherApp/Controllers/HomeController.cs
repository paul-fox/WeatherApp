using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{    
    public class HomeController : BaseController
    {
        public HomeController(ILogger<HomeController> logger, GetSettings myService)
            : base(logger, myService)
        {
        }

        public IActionResult Index()
        {
            _myService.PrintApiKey();
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

        [HttpPost]
        public ActionResult SearchLocation(string query)
        {
            List<LocationAPI> locationData = LocationAPIsController.GetLocation(query);
            List<WeatherAPI.Root> weatherData = WeatherAPIsController.GetWeather(locationData);
            return View("Index", weatherData);
        }
    }
}
