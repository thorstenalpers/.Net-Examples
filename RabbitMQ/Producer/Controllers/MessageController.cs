using Microsoft.AspNetCore.Mvc;
using MassTransit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Events;
using Microsoft.Extensions.Logging;

namespace Producer.Controllers
{
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

        [HttpGet]
        public async Task Post([FromBody] string message)
        {
            _logger.LogInformation($"User submitted a message \"{message}\"");
            await _publishEndpoint.Publish<MessageReceived>(new
            {
                Value = message
            });
        }

        [HttpGet]
        public async Task<List<string>> Get()
        {
            _logger.LogInformation("User requested all messages");
            await Task.CompletedTask;
            return new List<string>();
            //await Task.FromResult(new[]{""}.ToList());
            //List<string> list = new List<string>();
            //await _publishEndpoint. <MessageReceived>(new
            //{
            //    Value = message
            //});
        }
    }
}
