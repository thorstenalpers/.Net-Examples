using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.MediatR.Core.Behaviors
{
    public class GenericPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
		private readonly ILogger _logger;

		public GenericPipelineBehavior(ILogger logger)
        {
			_logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var timer = Stopwatch.StartNew();
            Debug.WriteLine($"GenericPipelineBehavior: request={request}");
            var response = await next();
            timer.Stop();
            Debug.WriteLine($"GenericPipelineBehavior: response={response}, ExecutionTimeInSeconds = {timer.Elapsed.Seconds}");
            _logger.LogInformation($"A Request of type {request.GetType().Name} tracked", new Dictionary<string, string>() { { "ExecutionTime", $"{timer.Elapsed.Seconds}" } });
            return response;
        }
    }
}