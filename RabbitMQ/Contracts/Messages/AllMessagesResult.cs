using System;
using System.Collections.Generic;

namespace Contracts.Messages
{
    public interface AllMessagesResult
    {
        public Guid CorrelationId { get; set; }
        List<string> Messages { get; set; }
    }
}
