using Alpha.API.Controllers.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shortener.Services.Notifications;
using System;
using System.Linq;

namespace Alpha.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class WeatherForecastController : BaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(INotification notifier, ILogger<WeatherForecastController> logger) : base(notifier)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            //NotifyError("Teste 3");

            return CustomResponse(result);
        }
    }
}
