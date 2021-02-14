namespace Examples.NSwag.Consumer.Controllers
{
	using Examples.NSwag.Consumer.GeneratedClient;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	[ApiController]
	[Route("[controller]/[action]")]
	public class ConsumerController : ControllerBase
	{
		private readonly ILogger<ConsumerController> _logger;
		private readonly IWeatherForecastClient _weatherForecastClient;

		public ConsumerController(ILogger<ConsumerController> logger, IWeatherForecastClient weatherForecastClient)
		{
			_logger = logger;
			_weatherForecastClient = weatherForecastClient;
		}

		[HttpGet]
		public async Task<IEnumerable<WeatherForecast>> GetAll()
		{
			return await _weatherForecastClient.WeatherForecastAsync();
		}

		[HttpGet]
		public async Task<IEnumerable<WeatherForecast>> GetBySummary(string summary)
		{
			var weatherForecasts = await _weatherForecastClient.WeatherForecastAsync();
			return weatherForecasts.Where(w => w.Summary == summary);
		}
	}
}
