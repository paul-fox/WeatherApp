using Humanizer;
using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;

namespace WeatherApp
{
    public class Global
    {
        public static readonly string apiKey = "c519021477d8da674506cf35afe15814";
        public static readonly string locationURL = $"http://api.openweathermap.org/geo/1.0/direct";
        public static readonly string weatherURL = $"https://api.openweathermap.org/data/2.5/weather";
        public static readonly string iconURL = $"https://openweathermap.org/img/wn/";
        public static readonly int queryLimit = 5;
        public static readonly string timeFormat = $"ddd, dd MMM yyyy HH:mm:ss";

        public static double KelvinToFahrenheit(double kelvin)
        {
            return Math.Round((kelvin - 273.15) * 9 / 5 + 32, 0);
        }
        
        public static double MpsToMph(double mps)
        {
            return Math.Round((mps * 2.23694), 1);
        }
        
        public static double MetersToMiles(double meters)
        {
            return Math.Round((meters / 1609), 1);
        }
        
        public static int SecToHours(int sec)
        {
            return sec/3600;
        }

        public static string IconToUrl(string icon)
        {
            return iconURL + icon + ".png";
        }

        public static string HtmlToString(IHtmlContent htmlContent)
        {
            using var writer = new StringWriter();
            htmlContent.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }       
        
        public static double HtmlToDouble(IHtmlContent htmlContent)
        {
            using var writer = new StringWriter();
            htmlContent.WriteTo(writer, HtmlEncoder.Default);
            return Convert.ToDouble(writer.ToString());
        }        
        
        public static int HtmlToInt(IHtmlContent htmlContent)
        {
            using var writer = new StringWriter();
            htmlContent.WriteTo(writer, HtmlEncoder.Default);
            return Int32.Parse(writer.ToString());
        }

        public static List<TimeZoneInfo> FindTimeZoneByOffset(int offsetHours)
        {
            TimeSpan targetOffset = TimeSpan.FromHours(offsetHours);
            DateTime nowUtc = DateTime.UtcNow;
            var matches = new List<TimeZoneInfo>();

            foreach (TimeZoneInfo timeZone in TimeZoneInfo.GetSystemTimeZones())
            {
                TimeSpan currentOffset = timeZone.GetUtcOffset(nowUtc);
                
                if (currentOffset == targetOffset)
                {
                    matches.Add(timeZone);
                }
            }
            return matches;
        }
    }
}
