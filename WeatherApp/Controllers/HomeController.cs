using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(
            ILogger<HomeController> logger,
            IOptions<MySettingsModel> mySettings,
            ILocationApiService locationApiService,
            IWeatherApiService weatherApiService,
            IWeatherSqlService weatherSqlService)
            : base(logger, mySettings, locationApiService, weatherApiService, weatherSqlService)
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
        public async Task<IActionResult> SearchLocation(string query)
        {
            var locationData = _locationApiService.GetLocation(query);
            var weatherData = _weatherApiService.GetWeather(locationData);
            await _weatherSqlService.InsertWeather(locationData, weatherData);

            return View("Index", weatherData);
        }
    }
}
