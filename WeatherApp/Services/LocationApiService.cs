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

        public List<LocationApiModel> GetLocation(string query)
        {
            if (query == null)
            {
                return null;
            }
            else
            {
                string[] queries = query.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string locationParameters = $"?q={queries[0]}&limit={_settings.QueryLimit}&appid={_settings.ApiKey}";
                List<LocationApiModel> locationData = [];

                if (queries.Length == 3)
                {
                    locationParameters = $"?q={queries[0]},{queries[1]},{queries[2]}&limit={_settings.QueryLimit}&appid={_settings.ApiKey}";
                }
                else if (queries.Length == 2)
                {
                    locationParameters = $"?q={queries[0]},{queries[1]},{""}&limit={_settings.QueryLimit}&appid={_settings.ApiKey}";
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_settings.LocationURL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(locationParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var dataObjects = response.Content.ReadAsAsync<IEnumerable<LocationApiModel>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    foreach (var data in dataObjects)
                    {
                        locationData.Add(data);
                        /*                        Debug.WriteLine($"City: {data.Name.ToString()}\t Country: {data.Country.ToString()}\t State: {data.State.ToString()}\n" +
                                                    $"Latitude: {data.Lat.ToString()}\t Longitude: {data.Lon.ToString()}");*/
                    }
                }
                else
                {
                    Debug.WriteLine((int)response.StatusCode, response.ReasonPhrase);
                }

                // Make any other calls using HttpClient here.

                // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                client.Dispose();
                if (locationData.Count == 0)
                {
                    Debug.WriteLine("Location Not Found!");
                }

                return locationData;
            }
        }
    }
}
