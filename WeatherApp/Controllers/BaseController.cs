using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger<BaseController> _logger;
        protected readonly MySettingsModel _mySettings;
        protected readonly ILocationApiService _locationService;
        protected readonly IWeatherApiService _weatherService;

        protected BaseController(
            ILogger<BaseController> logger,
            IOptions<MySettingsModel> mySettings,
            ILocationApiService locationService,
            IWeatherApiService weatherService)
        {
            _logger = logger;
            _mySettings = mySettings.Value;
            _locationService = locationService;
            _weatherService = weatherService;
        }
    }
}
