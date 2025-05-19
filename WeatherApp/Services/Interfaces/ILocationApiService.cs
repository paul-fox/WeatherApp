using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces
{
    public interface ILocationApiService
    {
        Task<List<LocationApiModel>?> GetLocationAsync(string query);
    }
}
