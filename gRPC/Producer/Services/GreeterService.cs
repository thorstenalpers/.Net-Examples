using Examples.GRPC.Greet;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static Examples.GRPC.Greet.Greeter;

namespace Examples.gRPC.Producer.Services
{
	public class GreeterService : GreeterBase
    {
        private readonly ILogger _logger;

        public GreeterService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GreeterService>();
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Sending hello to {request.Name}");
            return Task.FromResult(new HelloReply { Message = $"Hello {request.Name}" });
        }

        public override Task<HelloReply> SayHelloFrom(HelloRequestFrom request, ServerCallContext context)
        {
            _logger.LogInformation($"Sending hello to {request.Name} from {request.From}");
            return Task.FromResult(new HelloReply { Message = $"Hello {request.Name} from {request.From}" });
        }
    }
}