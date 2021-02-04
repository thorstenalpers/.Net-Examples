using Core.Requests;
using MediatR;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.RequestHandler
{
    public class SomeRequestHandler : IRequestHandler<SomeRequest, bool>
    {
        public async Task<bool> Handle(SomeRequest request, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"SomeRequestHandler - Received SomeRequest with Message: {request.Message}");
            Random random = new Random();
            await Task.Delay(TimeSpan.FromSeconds(random.Next(2, 5)));
            Debug.WriteLine($"SomeRequestHandler - End");
            return true;
        }
    }
}
