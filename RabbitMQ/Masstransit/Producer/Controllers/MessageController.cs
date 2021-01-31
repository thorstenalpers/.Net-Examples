namespace Masstransit.Producer.Controllers
{
	using Masstransit.Contracts.Events;
	using MassTransit;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	[ApiController]
	[Route("[controller]")]
	public class MessageController : ControllerBase
	{
		readonly IPublishEndpoint _publishEndpoint;
		private readonly ILogger<MessageController> _logger;

		public MessageController(ILogger<MessageController> logger, IPublishEndpoint publishEndpoint)
		{
			_publishEndpoint = publishEndpoint;
			_logger = logger;
		}

		[HttpPost]
		public async Task<ActionResult> Post(string message)
		{
			_logger.LogInformation($"User submitted a message \"{message}\"");
			await _publishEndpoint.Publish<MessageReceived>(new
			{
				Value = message
			});
			return Ok();
		}
	}
}
