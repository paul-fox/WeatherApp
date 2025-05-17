using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger<BaseController> _logger;
        protected readonly MySettingsModel _mySettings;
        protected readonly ILocationApiService _locationApiService;
        protected readonly IWeatherApiService _weatherApiService;
        protected readonly IWeatherSqlService _weatherSqlService;

        protected BaseController(
            ILogger<BaseController> logger,
            IOptions<MySettingsModel> mySettings,
            ILocationApiService locationApiService,
            IWeatherApiService weatherApiService,
            IWeatherSqlService weatherSqlService)
        {
            _logger = logger;
            _mySettings = mySettings.Value;
            _locationApiService = locationApiService;
            _weatherApiService = weatherApiService;
            _weatherSqlService = weatherSqlService;
        }
    }
}
