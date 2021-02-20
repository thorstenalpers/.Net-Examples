using Examples.HealthCheck.WeatherService.Models;
using Examples.HealthCheck.WeatherService.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Examples.HealthCheck.WeatherService.Controllers
{
	[ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly HttpUrisOptions _httpUrisOptions;
        private readonly ExternalWeatherApiOptions _externalWeatherApiOptions;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly MyDbContext _myDbContext;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptionsMonitor<HttpUrisOptions> optionsMonitor,
            IOptionsMonitor<ExternalWeatherApiOptions> externalWeatherApiOptionsMonitor,
            IHttpClientFactory httpClientFactory,
			MyDbContext myDbContext)
        {
            _logger = logger;
            _httpUrisOptions = optionsMonitor.CurrentValue;
            _externalWeatherApiOptions = externalWeatherApiOptionsMonitor.CurrentValue;
			_httpClientFactory = httpClientFactory;
			_myDbContext = myDbContext;
		}

		[HttpGet]
        public IEnumerable<WeatherForecast> GetFromDatabase()
        {
			return _myDbContext.WeatherForecasts;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetFromRemoteDependency()
        {
            var client = _httpClientFactory.CreateClient("weatherforecast");
            var response = await client.GetAsync(_externalWeatherApiOptions.Uri);
            var weatherForecasts = await response.Content.ReadAsAsync<IEnumerable<WeatherForecast>>();
            return weatherForecasts;
        }
    }
}