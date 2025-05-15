namespace WeatherApp.Models
{
    public class LocationApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public string Country { get; set; }
        public string State { get; set; } = string.Empty;
    }
}