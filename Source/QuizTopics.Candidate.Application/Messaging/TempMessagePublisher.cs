using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizDesigner.Events;

namespace QuizTopics.Candidate.Application.Messaging
{
    public sealed class TempMessagePublisher : IMessagePublisher
    {
        private readonly ILogger<TempMessagePublisher> logger;

        public TempMessagePublisher(ILogger<TempMessagePublisher> logger)
        {
            this.logger = logger;
        }

        public Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
        {
            this.logger.LogInformation($"Called Temp publisher for {integrationEvent.GetType().FullName}");

            return Task.CompletedTask;
        }
    }
}