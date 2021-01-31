//using MassTransit;
//using Microsoft.Extensions.Logging;
//using NServiceBus.Contracts.Events;
//using System.Threading.Tasks;

//namespace NServiceBus.Consumer.Consumers
//{
//	public class MessageReceivedConsumer : IConsumer<MessageReceived>
//	{
//		private readonly ILogger<MessageReceivedConsumer> _logger;

//		public MessageReceivedConsumer(ILogger<MessageReceivedConsumer> logger)
//		{
//			_logger = logger;
//		}

//		public async Task Consume(ConsumeContext<MessageReceived> context)
//		{
//			_logger.LogInformation($"Received message \"{context.Message.Message}\" with id \"{context.Message.CorrelationId}\"");
//			GetAllMessageConsumer.Messages.Add(context.Message.Message);
//			await Task.CompletedTask;
//		}
//	}
//}
