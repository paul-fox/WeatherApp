namespace WeatherApp.Models
{
    public class MySettingsModel
    {
        public string ApiKey { get; set; }
        public string LocationUrlBase { get; set; }
        public string WeatherUrlBase { get; set; }
        public string IconUrlBase { get; set; }
        public int LocationQueryLimit { get; set; }
        public double WeatherUpdateIntervalMinutes { get; set; }
    }
}
