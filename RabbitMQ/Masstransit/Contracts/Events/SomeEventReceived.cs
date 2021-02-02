using System;

namespace Masstransit.Contracts.Events
{
	public interface SomeEventReceived
	{
		public Guid CorrelationId { get; set; }
		public string Message { get; set; }
	}
}
