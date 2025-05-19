using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services
{
    public class WeatherApiService : IWeatherApiService
    {
        private readonly MySettingsModel _settings;

        public WeatherApiService(IOptions<MySettingsModel> settings)
        {
            _settings = settings.Value;
        }

        public async Task<Root?> GetWeatherAsync(double lat, double lon)
        {
            if (double.IsNaN(lat) || double.IsNaN(lon))
            {
                return null;
            }

            var weatherParameters = $"?lat={lat}&lon={lon}&appid={_settings.ApiKey}";

            using var client = new HttpClient
            {
                BaseAddress = new Uri(_settings.WeatherURL)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            try
            {
                var response = await client.GetAsync(weatherParameters);

                if (response.IsSuccessStatusCode)
                {
                    var weatherData = await response.Content.ReadFromJsonAsync<Root>();
                    return weatherData;
                }
                else
                {
                    Debug.WriteLine($"Error: {(int)response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception fetching weather: {ex.Message}");
                return null;
            }
        }
    }
}
