using System;

namespace Contracts.Messages
{
    public interface GetAllMessages
    {
        public Guid CorrelationId { get; set; }
    }
}
