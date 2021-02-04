using MediatR;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.MediatR.Domain.NotificationHandler
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
