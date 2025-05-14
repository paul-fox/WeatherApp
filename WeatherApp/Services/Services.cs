using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WeatherApp.Services
{
    public class MySettings
    {
        public string ApiKey { get; set; }
    }

    public class GetSettings
    {
        private readonly MySettings _settings;
        private readonly ILogger<GetSettings> _logger;

        public GetSettings(ILogger<GetSettings> logger, IOptions<MySettings> options)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public void PrintApiKey()
        {
            _logger.LogInformation("API Key: {ApiKey}", _settings.ApiKey);
        }
    }
}
