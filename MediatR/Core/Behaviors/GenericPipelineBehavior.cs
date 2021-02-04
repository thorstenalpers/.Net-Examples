using MediatR;
using Microsoft.ApplicationInsights;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Behaviors
{
    public class GenericPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly TelemetryClient telemetry;

        public GenericPipelineBehavior(TelemetryClient telemetry)
        {
            this.telemetry = telemetry;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var timer = Stopwatch.StartNew();
            Debug.WriteLine($"GenericPipelineBehavior: request={request}");
            var response = await next();
            timer.Stop();
            Debug.WriteLine($"GenericPipelineBehavior: response={response}, ExecutionTimeInSeconds = {timer.Elapsed.Seconds}");
            this.telemetry.TrackEvent($"A Request of type {request.GetType().Name} tracked", new Dictionary<string, string>() { { "ExecutionTime", $"{timer.Elapsed.Seconds}" } });
            return response;
        }
    }
}