using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces
{
    public interface IWeatherSqlService
    {
        Task InsertWeatherAsync(List<LocationApiModel> locationData, Root weatherData);
        Task<List<WeatherSqlModel>> GetAllWeatherAsync();
        Task<bool> IsSavedLocationAsync(int cityId);
        Task UpdateAllWeatherAsync();
        Task DeleteWeatherAsync(int weatherId);
    }
}
