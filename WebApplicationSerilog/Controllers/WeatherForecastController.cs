namespace WebApplicationSerilog.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Serilog;


    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ILogger _log;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, ILogger log)
        {
            _logger = logger;
            _log = log;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            ILogger logger = _log
                .ForContext("Name", "No");

            logger.Information("ay 7aga");

            throw new Exception("xxxxxxxxx");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }








        //    [HttpGet]
        //    [Route("LoggTest")]
        //    public IActionResult LoggTest()
        //    {
        //        _logger.LogInformation("normal log");
        //        int count;
        //        //try
        //        //{
        //    
        //            for (count = 0; count <= 1; count++)
        //            {
        //                if (count == 1)
        //                {
        //                    throw new Exception("default test exception");
        //                }
        //            }
        //            return Ok();
        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    return Ok(new ErrorResponse
        //        //    {
        //        //        Message = ex.Message,
        //        //        StackTrace = ex.StackTrace,
        //        //    });
        //        //    //_logger.LogError(ex, "Exception catch log");
        //        //    //return BadRequest(ex.Message);
        //        //}
        //    }
        //}

        //public class ErrorResponse
        //{
        //    public string Message { get; set; }
        //    public string StackTrace { get; set; }
        //}
    }
}