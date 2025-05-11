using System.Diagnostics;
using System.Net.Http.Headers;

namespace WeatherApp.Models
{
    public class LocationSQL
    {
        public int Id { get; set; }
        public string City { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public LocationSQL(int id, string city, float latitude, float longitude, string country, string state)
        {
            Id = id;
            City = city;
            Latitude = latitude;
            Longitude = longitude;
            Country = country;
            State = state;
        }
    }
}