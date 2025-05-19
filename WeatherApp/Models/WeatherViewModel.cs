namespace WeatherApp.Models
{
    public class WeatherViewModel
    {
        // Location
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        // Time
        public DateTime LocalTime { get; set; }
        public string LocalTimeZone { get; set; }
        public DateTime LocationTime { get; set; }
        public string LocationTimeZone { get; set; }

        public DateTime SunriseLocalTime { get; set; }
        public DateTime SunriseLocationTime { get; set; }
        public DateTime SunsetLocalTime { get; set; }
        public DateTime SunsetLocationTime { get; set; }

        // Weather
        public string Weather { get; set; }
        public string WeatherDescription { get; set; }
        public string WeatherIconUrl { get; set; }

        // Temperature
        public double TempFahrenheit { get; set; }
        public double FeelsLikeFahrenheit { get; set; }
        public double TempMinFahrenheit { get; set; }
        public double TempMaxFahrenheit { get; set; }

        // Misc.
        public int Humidity { get; set; }
        public double Visibility { get; set; }
        public string VisibilityUnit { get; set; }
        public double WindSpeedMph { get; set; }
        public int Cloudiness { get; set; }
    }
}

