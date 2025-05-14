using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using static WeatherApp.Models.WeatherAPI;

namespace WeatherApp.Controllers
{
    public class WeatherAPIsController : Controller
    {
        [HttpPost]
        public static List<Root> GetWeather(List<LocationAPI> locationData)
        {
            if (locationData == null)
            {
                return null;
            }
            else
            {
                string weatherParameters = $"?lat={locationData[0].Lat.ToString()}&lon={locationData[0].Lon.ToString()}&appid={Global.apiKey}";
            
                List<Root> weatherData = [];

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Global.weatherURL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(weatherParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var data = response.Content.ReadAsAsync<Root>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    Root myDeserializedClass = data;
                    weatherData.Add(data);
/*                    Debug.WriteLine($"Weather: {data.weather[0].main.ToString()}, {data.weather[0].description.ToString()}\n" +
                        $"Temperature: {Math.Round((data.main.temp - 273.15) * 9 / 5 + 32, 2)} Fahrenheit\n" +
                        $"City: {data.name.ToString()}\n" +
                        $"Datetime: {DateTimeOffset.FromUnixTimeSeconds(data.dt).ToLocalTime()}");*/
                }
                else
                {
                    Debug.WriteLine((int)response.StatusCode, response.ReasonPhrase);
                }

                // Make any other calls using HttpClient here.

                // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                client.Dispose();
                if (weatherData.Count == 0)
                {
                    Debug.WriteLine("Weather Not Found!");
                }
                return weatherData;
            }
        }
    }
}
