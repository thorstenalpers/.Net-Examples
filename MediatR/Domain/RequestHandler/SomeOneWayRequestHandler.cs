using Core.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.RequestHandler
{
    public class SomeOneWayRequestHandler : AsyncRequestHandler<SomeOneWayRequest>
    {
        protected override async Task Handle(SomeOneWayRequest request, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"SomeOneWayRequestHandler - Received SomeOneWayRequest with Message: {request.Message}");
            Random random = new Random();
            await Task.Delay(TimeSpan.FromSeconds(random.Next(2, 5)));
            Debug.WriteLine($"SomeOneWayRequestHandler - End");
        }
    }
}
