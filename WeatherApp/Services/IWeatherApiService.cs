using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IWeatherApiService
    {
        List<WeatherApiModel.Root> GetWeather(List<LocationApiModel> locationData);
    }
}
