using System;

namespace EasyNetQ.Contracts.Events
{
	public interface MessageReceived
	{
		public Guid CorrelationId { get; set; }
		public string Message { get; set; }
	}
}
