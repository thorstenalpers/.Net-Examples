using Examples.HealthCheck.WeatherService.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Examples.HealthCheck.WeatherService.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;
		private readonly HttpUrisOptions _httpUrisOptions;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptionsMonitor<HttpUrisOptions> optionsMonitor)
		{
			_logger = logger;
			_httpUrisOptions = optionsMonitor.CurrentValue;
		}

		[HttpGet]
		public IEnumerable<WeatherForecast> GetFromDatabase()
		{
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			})
			.ToArray();
		}

		[HttpGet]
		public IEnumerable<WeatherForecast> GetFromRemoteDependency()
		{
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}