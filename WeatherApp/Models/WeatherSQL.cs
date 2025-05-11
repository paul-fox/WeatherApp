using System.Diagnostics;
using System.Net.Http.Headers;

namespace WeatherApp.Models
{
    public class WeatherSQL
    {
        public int Id { get; set; }
        public string? WeatherDisc { get; set; }
        public int? Main { get; set; }
        public string? Datetime { get; set; }
        public int? CityId { get; set; }
        public string? City { get; set; }
    }
}
