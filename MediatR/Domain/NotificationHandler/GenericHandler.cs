using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorHandler.NotificationHandler
{
    public class GenericHandler : INotificationHandler<INotification>
    {
        public Task Handle(INotification notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"GenericHandler - Received INotification={notification}");
            return Task.CompletedTask;
        }
    }
}
