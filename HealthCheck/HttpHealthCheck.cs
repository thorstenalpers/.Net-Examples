using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TutorialHealthCheck.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TutorialHealthCheck
{
    public class HttpHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HealtCheckOptions _remoteOptions;

        public HttpHealthCheck(IHttpClientFactory httpClientFactory, IOptions<HealtCheckOptions> remoteOptions)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _remoteOptions = remoteOptions?.Value ?? throw new ArgumentNullException(nameof(remoteOptions));
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using HttpClient client = _httpClientFactory.CreateClient();
            var healthyResult = new Dictionary<string, string>();
            var isHealthy = true;

            foreach (var uri in _remoteOptions.RemoteDependencies)
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
            string description = JsonConvert.SerializeObject(healthyResult, Formatting.Indented);
            return (isHealthy) ? HealthCheckResult.Healthy(description) : HealthCheckResult.Unhealthy(description);
        }
    }
}
