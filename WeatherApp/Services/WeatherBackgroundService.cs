using Microsoft.Extensions.Options;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services
{
    public class WeatherBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly MySettingsModel _settings;
        private readonly TimeSpan _interval;

        public WeatherBackgroundService(IServiceProvider serviceProvider, IOptions<MySettingsModel> settings)
        {
            _serviceProvider = serviceProvider;
            _settings = settings.Value;
            _interval = TimeSpan.FromMinutes(_settings.WeatherUpdateIntervalMinutes);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var weatherService = scope.ServiceProvider.GetRequiredService<IWeatherSqlService>();

                    try
                    {
                        await weatherService.UpdateAllWeatherAsync();
                        Console.WriteLine($"[{DateTime.Now}] Weather data updated.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Weather update failed: {ex.Message}");
                    }
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
