using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces
{
    public interface IWeatherApiService
    {
        Task<Root?> GetWeatherAsync(double lat, double lon);
    }
}
