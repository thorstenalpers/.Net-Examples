namespace Examples.gRPC.Producer.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using System.Threading.Tasks;

	[ApiController]
	[Route("[controller]/[action]")]
	public class ConsumerController : ControllerBase
	{
		private readonly ILogger<ConsumerController> _logger;

		public ConsumerController(ILogger<ConsumerController> logger)
		{
			_logger = logger;
		}

		[HttpPost]
		public async Task<ActionResult> Post(string message)
		{
			_logger.LogInformation($"User submitted a message \"{message}\"");
			return await Task.FromResult(Ok());
		}
	}
}
