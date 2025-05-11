using Microsoft.AspNetCore.Html;
using System.Drawing;
using System.Text.Encodings.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WeatherApp
{
    public class Global
    {
        public static readonly string apiKey = "c519021477d8da674506cf35afe15814";
        public static readonly string locationURL = $"http://api.openweathermap.org/geo/1.0/direct";
        public static readonly string weatherURL = $"https://api.openweathermap.org/data/2.5/weather";
        public static readonly int queryLimit = 5;

        public static double KelvinToFahrenheit(double kelvin)
        {
            return Math.Round((kelvin - 273.15) * 9 / 5 + 32, 0);
        }
        
        public static double MpsToMph(double mps)
        {
            return Math.Round((mps * 2.23694), 1);
        }

        public static string IconToUrl(string icon)
        {

            return ("https://openweathermap.org/img/wn/" + icon + ".png");
        }
/*        public async Task<Bitmap> IconToUrl(string icon)
        {
            var url = "https://openweathermap.org/img/wn/" + icon + ".png";
            var httpClient = new HttpClient();
            var stream = await httpClient.GetStreamAsync(url);
            return new Bitmap(stream);
        }*/

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
    }
}
