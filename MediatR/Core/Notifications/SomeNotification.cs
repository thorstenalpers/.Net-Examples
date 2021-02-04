using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Notifications
{
    public class SomeNotification : INotification
    {
        public SomeNotification(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
