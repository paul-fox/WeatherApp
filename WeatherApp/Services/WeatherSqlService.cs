using Microsoft.EntityFrameworkCore;
using WeatherApp.Context;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services
{
    public class WeatherSqlService : IWeatherSqlService
    {
        private readonly ApplicationDbContext _dbContext;
        protected readonly IWeatherApiService _weatherApiService;

        public WeatherSqlService(ApplicationDbContext dbContext, IWeatherApiService weatherApiService)
        {
            _dbContext = dbContext;
            _weatherApiService = weatherApiService;
        }

        public static WeatherSqlModel MapToSqlModel(LocationApiModel locationData, Root weatherData)
        {
            if ((locationData == null) || (weatherData == null)) return null;

            return new WeatherSqlModel
            {
                // Geo
                CityId = weatherData.id,
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

        public async Task InsertWeatherAsync(List<LocationApiModel> locationData, Root weatherData)
        {
            var weatherEntry = MapToSqlModel(locationData.FirstOrDefault(), weatherData);
            if (weatherEntry != null)
            {
                _dbContext.WeatherEntries.Add(weatherEntry);
                await _dbContext.SaveChangesAsync();
                //_logger.LogInformation("Weather data saved to SQL Server.");
            }
        }

        public async Task<List<WeatherSqlModel>> GetAllWeatherAsync()
        {
            return await _dbContext.WeatherEntries.ToListAsync();
        }

        public async Task<bool> IsSavedLocationAsync(int cityId)
        {
            return await _dbContext.WeatherEntries.AnyAsync(e =>
                e.CityId == cityId);
        }

        public async Task UpdateAllWeatherAsync()
        {
            var allLocations = await _dbContext.WeatherEntries.ToListAsync();

            foreach (var location in allLocations)
            {
                // Fetch fresh weather data using lat/lon
                var updatedWeather = await _weatherApiService.GetWeatherAsync(location.Lat, location.Lon);

                // Update the fields
                // Time
                location.DateTime = updatedWeather.dt;
                location.TimeZone = updatedWeather.timezone;
                location.Sunrise = updatedWeather.sys?.sunrise ?? 0;
                location.Sunset = updatedWeather.sys?.sunset ?? 0;

                // Weather
                location.Weather = updatedWeather.weather?.FirstOrDefault()?.main;
                location.WeatherDescription = updatedWeather.weather?.FirstOrDefault()?.description;
                location.WeatherIcon = updatedWeather.weather?.FirstOrDefault()?.icon;

                // Temp
                location.Temp = updatedWeather.main?.temp ?? 0;
                location.FeelsLike = updatedWeather.main?.feels_like ?? 0;
                location.TempMin = updatedWeather.main?.temp_min ?? 0;
                location.TempMax = updatedWeather.main?.temp_max ?? 0;

                // Misc.
                location.Humidity = updatedWeather.main?.humidity ?? 0;
                location.Visibility = updatedWeather.visibility;
                location.WindSpeed = updatedWeather.wind?.speed ?? 0;
                location.Cloudiness = updatedWeather.clouds?.all ?? 0;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteWeatherAsync(int weatherId)
        {
            var weather = await _dbContext.WeatherEntries.FindAsync(weatherId);
            if (weather != null)
            {
                _dbContext.WeatherEntries.Remove(weather);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
