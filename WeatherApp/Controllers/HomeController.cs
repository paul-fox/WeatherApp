using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;
using WeatherApp.Utilities;

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

        [HttpGet]
        public async Task<IActionResult> WeatherTablePartial()
        {
            var weatherSqlModels = await _weatherSqlService.GetAllWeatherAsync();

            var weatherViewModels = weatherSqlModels.Select(entry =>
            {
                var timeZone = ConversionUtil.FindTimeZoneByOffset(ConversionUtil.SecToHours(entry.TimeZone)).FirstOrDefault() ?? TimeZoneInfo.Local;

                var utcTime = DateTimeOffset.FromUnixTimeSeconds(entry.DateTime).UtcDateTime;
                var sunriseUtc = DateTimeOffset.FromUnixTimeSeconds(entry.Sunrise).UtcDateTime;
                var sunsetUtc = DateTimeOffset.FromUnixTimeSeconds(entry.Sunset).UtcDateTime;

                var (visibility, visibilityUnit) = ConversionUtil.VisibilityConversion(entry.Visibility);

                return new WeatherViewModel
                {
                    // Location
                    Id = entry.Id,
                    City = entry.City,
                    Country = entry.Country,
                    State = entry.State,

                    // Time
                    LocalTime = utcTime.ToLocalTime(),
                    LocalTimeZone = TimeZoneInfo.Local.DisplayName,
                    LocationTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZone),
                    LocationTimeZone = timeZone.DisplayName,

                    SunriseLocalTime = TimeZoneInfo.ConvertTimeFromUtc(sunriseUtc, TimeZoneInfo.Local),
                    SunriseLocationTime = TimeZoneInfo.ConvertTimeFromUtc(sunriseUtc, timeZone),
                    SunsetLocalTime = TimeZoneInfo.ConvertTimeFromUtc(sunsetUtc, TimeZoneInfo.Local),
                    SunsetLocationTime = TimeZoneInfo.ConvertTimeFromUtc(sunsetUtc, timeZone),

                    // Weather
                    Weather = entry.Weather,
                    WeatherDescription = entry.WeatherDescription,
                    WeatherIconUrl = $"{_mySettings.IconUrlBase}{entry.WeatherIcon}.png",

                    // Temp
                    TempFahrenheit = ConversionUtil.KelvinToFahrenheit(entry.Temp),
                    FeelsLikeFahrenheit = ConversionUtil.KelvinToFahrenheit(entry.FeelsLike),
                    TempMinFahrenheit = ConversionUtil.KelvinToFahrenheit(entry.TempMin),
                    TempMaxFahrenheit = ConversionUtil.KelvinToFahrenheit(entry.TempMax),

                    // Misc.
                    Humidity = entry.Humidity,
                    Visibility = visibility,
                    VisibilityUnit = visibilityUnit,
                    WindSpeedMph = ConversionUtil.MpsToMph(entry.WindSpeed),
                    Cloudiness = entry.Cloudiness
                };
            }).ToList();

            return PartialView("_WeatherTable", weatherViewModels);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchLocationAsync(string query)
        {
            try
            {
                var locationData = await _locationApiService.GetLocationAsync(query);

                // Check if no location is found
                if (locationData == null || !locationData.Any())
                {
                    TempData["ErrorMessage"] = "Location not found. Please try another search.";
                    return RedirectToAction("Index");
                }

                var weatherData = await _weatherApiService.GetWeatherAsync(locationData.First().lat, locationData.First().lon);

                // Check if weatherData is null
                if (weatherData == null)
                {
                    TempData["ErrorMessage"] = "Weather data could not be retrieved.";
                    return RedirectToAction("Index");
                }

                bool exists = await _weatherSqlService.IsSavedLocationAsync(weatherData.id);

                if (!exists)
                {
                    await _weatherSqlService.InsertWeatherAsync(locationData, weatherData);
                }
                else
                {
                    TempData["ErrorMessage"] = "Location already exists. Please try another search.";
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while searching for the location.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAllWeatherAsync()
        {
            await _weatherSqlService.UpdateAllWeatherAsync();
            _logger.LogInformation("Updated weather");
            return Ok();
        }

        public class DeleteRequest
        {
            public int WeatherId { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWeatherAsync([FromBody] DeleteRequest request)
        {
            var weatherId = request.WeatherId;
            await _weatherSqlService.DeleteWeatherAsync(weatherId);
            _logger.LogInformation("Deleting weather ID: {Id}", weatherId);
            return Ok();
        }
    }
}
