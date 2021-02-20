using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Tutorials.HealthCheck.TestApp.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestHealthCheckController : ControllerBase
    {
        private readonly ILogger<TestHealthCheckController> _logger;

        public TestHealthCheckController(ILogger<TestHealthCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string StartPollingHealthChecks()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int cntChecks = 0;
            int cntUnheathyChecks = 0;
            while (stopwatch.Elapsed < TimeSpan.FromSeconds(100))
            {
                cntChecks++;

                if (true)
                {
                    cntUnheathyChecks++;
                }
            }
            stopwatch.Stop();

            return $"Total time in secconds of {stopwatch.Elapsed.Seconds}\n" +
                    $"Number of healthy checks {cntUnheathyChecks} of {cntChecks}";
        }
    }
}