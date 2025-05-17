using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces
{
    public interface IWeatherApiService
    {
        List<WeatherApiModel.Root> GetWeather(List<LocationApiModel> locationData);
    }
}
