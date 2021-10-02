using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizDesigner.Common.Database;
using QuizDesigner.Common.Outbox;
using QuizDesigner.Events;
using QuizTopics.Candidate.Application.Messaging;

namespace QuizTopics.Candidate.Application.Outbox
{
    public class OutboxService : IOutboxService
    {
        private readonly IDbContext context;
        private readonly IMessagePublisher messagePublisher;
        private readonly IOutboxRepository outboxRepository;
        private readonly ILogger<OutboxService> logger;

        public OutboxService(
            IDbContext context, 
            IMessagePublisher messagePublisher,
            Func<DbConnection, IOutboxRepository> outboxRepositoryFactory,
            ILogger<OutboxService> logger)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));

            var factory = outboxRepositoryFactory ?? throw new ArgumentNullException(nameof(outboxRepositoryFactory));
            this.outboxRepository = factory(context.DatabaseFacade.GetDbConnection());

            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddEventAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
        {
            await this.outboxRepository.SaveMessageAsync(integrationEvent, this.context.GetCurrentTransaction(), cancellationToken).ConfigureAwait(false);
        }

        public async Task PublishTransactionEventsAsync(Guid transactionId, CancellationToken cancellationToken = default)
        {
            var pendingOutboxMessages = await this.outboxRepository.GetNotPublishedAsync(transactionId, cancellationToken).ConfigureAwait(false);
            if(pendingOutboxMessages.TryGetValue(out var outboxMessages))
            {
                foreach (var outboxMessage in outboxMessages)
                {
                    await this.TrySendMessageAsync(outboxMessage, cancellationToken).ConfigureAwait(false);
                }
            }
            else
            {
                this.logger.LogDebug($"No outbox messages to send with transaction id: {transactionId}");
            }
        }

        public async Task PublishPendingEventsAsync(CancellationToken cancellationToken = default)
        {
            var pendingOutboxMessages = await this.outboxRepository.GetNotPublishedAsync(cancellationToken).ConfigureAwait(false);
            if (pendingOutboxMessages.TryGetValue(out var outboxMessages))
            {
                foreach (var outboxMessage in outboxMessages)
                {
                    await this.TrySendMessageAsync(outboxMessage, cancellationToken).ConfigureAwait(false);
                }
            }
            else
            {
                this.logger.LogDebug($"No outbox messages pending to publish");
            }
        }

        private async Task TrySendMessageAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken = default)
        {
            try
            {
                var message = OutboxSerializer.Deserialize<IIntegrationEvent>(outboxMessage);

                await this.outboxRepository.MarkMessageAsInProgressAsync(outboxMessage.Id, cancellationToken).ConfigureAwait(false);

                await this.messagePublisher.PublishAsync(message, cancellationToken).ConfigureAwait(false);

                await this.outboxRepository.MarkMessageAsPublishedAsync(outboxMessage.Id, cancellationToken).ConfigureAwait(false);

                this.logger.LogInformation($"Outbox message sent, id: {outboxMessage.Id}");
            }
            catch (OperationCanceledException e)
            {
                this.logger.LogError(e, $"Error publishing message, id: {outboxMessage.Id}");

                await this.outboxRepository.MarkMessageAsFailedAsync(outboxMessage.Id, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}