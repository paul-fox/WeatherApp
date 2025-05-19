using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services
{
    public class LocationApiService : ILocationApiService
    {
        private readonly MySettingsModel _settings;

        public LocationApiService(IOptions<MySettingsModel> settings)
        {
            _settings = settings.Value;
        }

        public async Task<List<LocationApiModel>?> GetLocationAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return null;

            string[] queries = query.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string locationParameters;

            if (queries.Length == 3)
                locationParameters = $"?q={queries[0]},{queries[1]},{queries[2]}&limit={_settings.QueryLimit}&appid={_settings.ApiKey}";
            else if (queries.Length == 2)
                locationParameters = $"?q={queries[0]},{queries[1]},{""}&limit={_settings.QueryLimit}&appid={_settings.ApiKey}";
            else
                locationParameters = $"?q={queries[0]}&limit={_settings.QueryLimit}&appid={_settings.ApiKey}";

            using var client = new HttpClient
            {
                BaseAddress = new Uri(_settings.LocationURL)
            };

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            try
            {
                var response = await client.GetAsync(locationParameters);

                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = await response.Content.ReadFromJsonAsync<List<LocationApiModel>>();
                    if (dataObjects == null || !dataObjects.Any())
                    {
                        Debug.WriteLine("Location Not Found!");
                        return [];
                    }

                    return dataObjects;
                }
                else
                {
                    Debug.WriteLine($"Error: {(int)response.StatusCode} - {response.ReasonPhrase}");
                    return [];
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception fetching location: {ex.Message}");
                return [];
            }
        }
    }
}
