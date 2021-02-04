using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.MediatR.Core.Behaviors
{
    public class GenericRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"GenericRequestPreProcessor: request={request}");
            return Task.CompletedTask;
        }
    }
}
