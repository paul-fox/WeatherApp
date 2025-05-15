using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class ConversionService
    {
        public static readonly string iconURL = $"https://openweathermap.org/img/wn/";
        public static readonly string timeFormat = $"ddd, dd MMM yyyy HH:mm:ss";

        private readonly MySettingsModel _settings;

        public ConversionService(IOptions<MySettingsModel> settings)
        {
            _settings = settings.Value;
        }

        public static double KelvinToFahrenheit(double kelvin)
        {
            return Math.Round((kelvin - 273.15) * 9 / 5 + 32, 0);
        }

        public static double MpsToMph(double mps)
        {
            return Math.Round(mps * 2.23694, 1);
        }

        public static double MetersToMiles(double meters)
        {
            return Math.Round(meters / 1609, 1);
        }

        public static int SecToHours(int sec)
        {
            return sec / 3600;
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
            return int.Parse(writer.ToString());
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
