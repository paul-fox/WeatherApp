using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces
{
    public interface ILocationApiService
    {
        List<LocationApiModel> GetLocation(string query);
    }
}
