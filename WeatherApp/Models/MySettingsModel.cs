namespace WeatherApp.Models
{
    public class MySettingsModel
    {
        public string ApiKey { get; set; }
        public string LocationURL { get; set; }
        public string WeatherURL { get; set; }
        public string IconURL { get; set; }
        public int QueryLimit { get; set; }
        public string TimeFormat { get; set; }
    }
}
