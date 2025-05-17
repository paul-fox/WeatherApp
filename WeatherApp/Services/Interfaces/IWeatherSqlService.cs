using WeatherApp.Models;
using static WeatherApp.Models.WeatherApiModel;

namespace WeatherApp.Services.Interfaces
{
    public interface IWeatherSqlService
    {
        Task InsertWeather(List<LocationApiModel> locationData, List<Root> weatherData);
    }
}
