using interfuture.Data;
using Microsoft.AspNetCore.Mvc;

namespace interfuture.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public TaskController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return null;
        }
    }
}