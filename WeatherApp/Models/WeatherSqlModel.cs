namespace WeatherApp.Models
{
    public class WeatherSqlModel
    {
        //Geo
        public int Id { get; set; }
        public string City { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        //Time
        public int DateTime { get; set; }
        public int TimeZone { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }

        //Weather
        public string Weather { get; set; }
        public string WeatherDescription { get; set; }
        public string WeatherIcon { get; set; }

        //Temp
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }

        //Misc.
        public int Humidity { get; set; }
        public double Visibility { get; set; }
        public double WindSpeed { get; set; }
        public int Cloudiness { get; set; }
        
    }
}