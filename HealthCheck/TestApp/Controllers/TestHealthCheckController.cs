using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.HealthCheck.TestApp.Controllers
{
	[ApiController]
    [Route("[controller]/[action]")]
    public class TestHealthCheckController : ControllerBase
    {
		private readonly ILogger<TestHealthCheckController> _logger;
		private readonly IHttpClientFactory _httpClientFactory;

		public TestHealthCheckController(ILogger<TestHealthCheckController> logger, IHttpClientFactory httpClientFactory)
        {
			_logger = logger;
			_httpClientFactory = httpClientFactory;
		}

		[HttpGet]
        public async Task<string> TestHealthCheckWithDatabase()
        {
			Uri healthUri = new Uri("http://localhost:6619/health/ready");
			var healthCheckClient = _httpClientFactory.CreateClient("healthcheck");

			_logger.LogInformation("Start polling of HealthChecks ...");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int cntChecks = 0;
            int cntHeathyChecks = 0;
			var cancelTokenSource = new CancellationTokenSource();

			_ = Task.Factory.StartNew(() => StartPollingApiWithDatabaseDependency(cancelTokenSource.Token));
			_ = Task.Factory.StartNew(() => StartPollingApiWithRemoteDependency(cancelTokenSource.Token));

			while (stopwatch.Elapsed < TimeSpan.FromSeconds(100))
            {
				var response = await healthCheckClient.GetAsync(healthUri);

				if (response.IsSuccessStatusCode)
                {
                    cntHeathyChecks++;
                }
				cntChecks++;
				await Task.Delay(TimeSpan.FromMilliseconds(100));
			}
			cancelTokenSource.Cancel();
			stopwatch.Stop();

            var message = $"Total time in secconds of {stopwatch.Elapsed.Seconds}\n" +
                    $"Number of healthy checks {cntHeathyChecks} of {cntChecks}";

            _logger.LogInformation(message);
			_logger.LogInformation("End of polling of HealthChecks ...");
			_logger.LogInformation("---------------------------------.");

			return message;
        }

		private async Task StartPollingApiWithRemoteDependency(CancellationToken cancellationToken)
		{
			Uri weatherApiUri = new Uri("http://localhost:6619/weatherforecast/GetFromRemoteDependency");
			var weatherClient = _httpClientFactory.CreateClient("weatherservice");
			_logger.LogInformation("Start polling API with remote dependency ...");
			while (!cancellationToken.IsCancellationRequested)
			{
				var response = await weatherClient.GetAsync(weatherApiUri);

				if (!response.IsSuccessStatusCode)
				{
					_logger.LogError("No success response from weather API with remote dependency");
				}
			}
			_logger.LogInformation("End of polling API with remote dependency ...");
			_logger.LogInformation("---------------------------------.");
		}

		private async Task StartPollingApiWithDatabaseDependency(CancellationToken cancellationToken)
		{
			Uri weatherApiUri = new Uri("http://localhost:6619/weatherforecast/GetFromDatabase");
			var weatherClient = _httpClientFactory.CreateClient("weatherservice");
			_logger.LogInformation("Start polling API with DB dependencies ...");
			while (!cancellationToken.IsCancellationRequested)
			{
				var response = await weatherClient.GetAsync(weatherApiUri);

				if (!response.IsSuccessStatusCode)
				{
					_logger.LogError("No success response from weather API with DB dependencies");
				}
			}
			_logger.LogInformation("End of polling API with DB dependencies ...");
			_logger.LogInformation("---------------------------------.");
		}
	}
}