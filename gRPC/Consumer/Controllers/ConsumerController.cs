namespace Examples.gRPC.Producer.Controllers
{
	using Examples.GRPC.Greet;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using System.Threading.Tasks;

	[ApiController]
    [Route("[controller]")]
    public class ConsumerController : ControllerBase
    {
        private readonly ILogger<ConsumerController> _logger;
        private readonly Greeter.GreeterClient _client;

        public ConsumerController(ILogger<ConsumerController> logger, Greeter.GreeterClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpPost]
        public async Task<HelloReply> Post(string message)
        {
            _logger.LogInformation($"User submitted a message \"{message}\"");
            // Forward the call on to the greeter service
            var reply = await _client.SayHelloAsync(new HelloRequest
            {
                Name = message
            });

            return reply;
        }
    }
}