using System.Diagnostics;
using System.Net.Http.Headers;

namespace WeatherApp.Models
{
    public class LocationAPI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public string Country { get; set; }
        public string State { get; set; } = string.Empty;
    }
}