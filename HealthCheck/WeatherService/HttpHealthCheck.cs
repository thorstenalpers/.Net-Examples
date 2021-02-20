using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.HealthCheck.WeatherService
{
	public class HttpHealthCheck : IHealthCheck
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly Options.HealthCheckOptions _customHealthCheckOptions;

		public HttpHealthCheck(IHttpClientFactory httpClientFactory, IOptions<Options.HealthCheckOptions> customHealthCheckOptions)
		{
			_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
			_customHealthCheckOptions = customHealthCheckOptions?.Value ?? throw new ArgumentNullException(nameof(customHealthCheckOptions));
		}

		public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
		{
			using HttpClient client = _httpClientFactory.CreateClient();
			var healthyResult = new Dictionary<string, object>();
			var isHealthy = true;

			foreach (var uri in _customHealthCheckOptions.RemoteDependencies.HttpUris)
			{
				try
				{
					var response = await client.GetAsync(uri, cancellationToken);
					healthyResult.Add(uri.ToString(), $"StatusCode: {response.StatusCode}");
					if (!response.IsSuccessStatusCode)
					{
						isHealthy = false;
					}
				}
				catch (Exception ex)
				{
					healthyResult.Add(uri.ToString(), $"Exception: {ex?.Message}");
					isHealthy = false;
				}
			}
			return isHealthy ? HealthCheckResult.Healthy("Healthy", healthyResult) : HealthCheckResult.Unhealthy("Unhealthy", null, healthyResult);
		}
	}
}