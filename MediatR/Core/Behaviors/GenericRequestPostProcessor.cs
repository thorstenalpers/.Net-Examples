using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Behaviors
{
    public class GenericRequestPostProcessor<TRequest, TResponse>
            : IRequestPostProcessor<TRequest, TResponse>
    {

        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"GenericRequestPostProcessor: request={request} - response={response}");
            return Task.CompletedTask;
        }
    }
}
