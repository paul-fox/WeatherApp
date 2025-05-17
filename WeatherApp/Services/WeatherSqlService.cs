using WeatherApp.Context;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;
using static WeatherApp.Models.WeatherApiModel;

namespace WeatherApp.Services
{
    public class WeatherSqlService : IWeatherSqlService
    {
        private readonly ApplicationDbContext _context;

        public WeatherSqlService(ApplicationDbContext context)
        {
            _context = context;
        }

        public static WeatherSqlModel MapToSqlModel(LocationApiModel locationData, Root weatherData)
        {
            if ((locationData == null) || (weatherData == null)) return null;

            return new WeatherSqlModel
            {
                // Geo
                Id = weatherData.Id,
                City = locationData.name,
                Lat = locationData.lat,
                Lon = locationData.lon,
                Country = locationData.country,
                State = locationData.state,

                // Time
                DateTime = weatherData.dt,
                TimeZone = weatherData.timezone,
                Sunrise = weatherData.sys?.sunrise ?? 0,
                Sunset = weatherData.sys?.sunset ?? 0,

                // Weather
                Weather = weatherData.weather?.FirstOrDefault()?.main,
                WeatherDescription = weatherData.weather?.FirstOrDefault()?.description,
                WeatherIcon = weatherData.weather?.FirstOrDefault()?.icon,

                // Temp
                Temp = weatherData.main?.temp ?? 0,
                FeelsLike = weatherData.main?.feels_like ?? 0,
                TempMin = weatherData.main?.temp_min ?? 0,
                TempMax = weatherData.main?.temp_max ?? 0,

                // Misc.
                Humidity = weatherData.main?.humidity ?? 0,
                Visibility = weatherData.visibility,
                WindSpeed = weatherData.wind?.speed ?? 0,
                Cloudiness = weatherData.clouds?.all ?? 0
            };
        }

        public async Task InsertWeather(List<LocationApiModel> locationData, List<Root> weatherData)
        {
            var weatherEntry = MapToSqlModel(locationData[0], weatherData[0]);
            if (weatherEntry != null)
            {
                _context.Weathers.Add(weatherEntry);
                await _context.SaveChangesAsync();
                //_logger.LogInformation("Weather data saved to SQL Server.");
            }
        }
    }
}
