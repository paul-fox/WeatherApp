namespace WeatherApp.Models
{
    public class LocationApiModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
        public string state { get; set; } = string.Empty;
    }
}