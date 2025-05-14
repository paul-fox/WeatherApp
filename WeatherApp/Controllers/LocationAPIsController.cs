using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class LocationAPIsController : Controller
    {
        [HttpPost]
        public static List<LocationAPI> GetLocation(string query)
        {            
            if (query == null)
            {
                return null;
            }
            else
            {
                string[] queries = query.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string locationParameters = $"?q={queries[0]}&limit={Global.queryLimit}&appid={Global.apiKey}";
                List<LocationAPI> locationData = [];

                if (queries.Length == 3)
                {
                    locationParameters = $"?q={queries[0]},{queries[1]},{queries[2]}&limit={Global.queryLimit}&appid={Global.apiKey}";
                }
                else if (queries.Length == 2)
                {
                    locationParameters = $"?q={queries[0]},{queries[1]},{""}&limit={Global.queryLimit}&appid={Global.apiKey}";
                }

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Global.locationURL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(locationParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var dataObjects = response.Content.ReadAsAsync<IEnumerable<LocationAPI>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
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
