using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(
            ILogger<HomeController> logger,
            IOptions<MySettingsModel> mySettings,
            ILocationApiService locationService,
            IWeatherApiService weatherService)
            : base(logger, mySettings, locationService, weatherService)
        {
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

        [HttpPost]
        public ActionResult SearchLocation(string query)
        {
            var locationData = _locationService.GetLocation(query);
            var weatherData = _weatherService.GetWeather(locationData);
            return View("Index", weatherData);
        }
    }
}
