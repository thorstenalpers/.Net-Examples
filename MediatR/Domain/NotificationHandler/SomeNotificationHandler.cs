using Examples.MediatR.Core.Notifications;
using MediatR;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.MediatR.Domain.NotificationHandler
{
    public class SomeNotificationHandler : INotificationHandler<SomeNotification>
    {
        public async Task Handle(SomeNotification notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"SomeNotificationHandler - Received SomeNotification with Message: {notification.Message}");
            Random random = new Random();
            await Task.Delay(TimeSpan.FromSeconds(random.Next(2, 5)));
            Debug.WriteLine($"SomeNotificationHandler - End");
        }
    }
}
