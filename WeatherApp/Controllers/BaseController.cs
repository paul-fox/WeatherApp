using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{    
    public class BaseController : Controller
    {
        protected readonly ILogger _logger;
        protected readonly GetSettings _myService;

        protected BaseController(ILogger logger, GetSettings myService)
        {
            _logger = logger;
            _myService = myService;
        }
    }
}
