using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;

namespace WeatherApp.Utilities
{
    public static class ConversionUtil
    {
        public static double KelvinToFahrenheit(double kelvin)
            => Math.Round((kelvin - 273.15) * 9 / 5 + 32, 0);

        public static double MpsToMph(double mps)
            => Math.Round(mps * 2.23694, 1);

        public static double MetersToFeet(double meters)
            => Math.Round(meters * 3.28084, 0);    
        
        public static double MetersToMiles(double meters)
            => Math.Round(meters / 1609.34, 1);

        public static (double, string) VisibilityConversion(double meters)
        {
            if (MetersToMiles(meters) >= 1)
                return (MetersToMiles(meters), "mi");
            else
                return (MetersToFeet(meters), "ft");
        }

        public static long SecToHours(long sec)
            => sec / 3600;

        public static string HtmlToString(IHtmlContent htmlContent)
        {
            using var writer = new StringWriter();
            htmlContent.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }

        public static double HtmlToDouble(IHtmlContent htmlContent)
            => Convert.ToDouble(HtmlToString(htmlContent));

        public static int HtmlToInt(IHtmlContent htmlContent)
            => int.Parse(HtmlToString(htmlContent));

        public static List<TimeZoneInfo> FindTimeZoneByOffset(long offsetHours)
        {
            TimeSpan targetOffset = TimeSpan.FromHours(offsetHours);
            DateTime nowUtc = DateTime.UtcNow;

            return TimeZoneInfo
                .GetSystemTimeZones()
                .Where(tz => tz.GetUtcOffset(nowUtc) == targetOffset)
                .ToList();
        }

        public static string NormalizeString(string input)
        {
            return input?.Trim().ToLowerInvariant();
        }
    }
}
