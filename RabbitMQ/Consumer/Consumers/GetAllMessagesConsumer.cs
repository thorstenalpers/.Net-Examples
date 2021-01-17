using Contracts.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Consumer.Consumers
{
    public class GetAllMessageConsumer : IConsumer<GetAllMessages>
    {
        private readonly ILogger<GetAllMessageConsumer> _logger;

        /// <summary>
        /// simple in memory cache
        /// </summary>
        public static List<string> Messages = new List<string>();

        public GetAllMessageConsumer(ILogger<GetAllMessageConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GetAllMessages> context)
        {
            _logger.LogInformation($"Received message \"{context.Message.CorrelationId}\"");

            await context.RespondAsync<AllMessagesResult>(new
            {
                Messages = GetAllMessageConsumer.Messages
            });
        }
    }
}
