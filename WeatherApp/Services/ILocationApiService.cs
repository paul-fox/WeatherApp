using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface ILocationApiService
    {
        List<LocationApiModel> GetLocation(string query);
    }
}
