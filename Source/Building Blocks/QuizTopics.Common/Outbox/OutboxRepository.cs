using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QuizDesigner.Events;
using QuizTopics.Common.Monad;

namespace QuizTopics.Common.Outbox
{
    public sealed class OutboxRepository : IOutboxRepository, IDisposable
    {
        private readonly OutboxContext context;

        public OutboxRepository(DbConnection dbConnection)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException(nameof(dbConnection));
            }

            this.context = new OutboxContext(new DbContextOptionsBuilder<OutboxContext>()
                    .UseSqlServer(dbConnection).Options);
        }

        // TODO: maybe object instead of integration event?
        public async Task SaveMessageAsync(IIntegrationEvent integrationEvent, IDbContextTransaction transaction, CancellationToken cancellationToken = default)
        {
            if (integrationEvent == null)
            {
                throw new ArgumentNullException(nameof(integrationEvent));
            }

            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            await this.context.Database.UseTransactionAsync(transaction.GetDbTransaction(), cancellationToken).ConfigureAwait(false);

            var outboxMessage = GetOutboxMessage(integrationEvent, transaction.TransactionId);
            await this.context.AddAsync(outboxMessage, cancellationToken).ConfigureAwait(false);

            await this.context.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task MarkMessageAsInProgressAsync(Guid messageId, CancellationToken cancellationToken = default)
        {
            await this.UpdateStatusAsync(messageId, EventState.InProgress, cancellationToken).ConfigureAwait(false);
        }

        public async Task MarkMessageAsPublishedAsync(Guid messageId, CancellationToken cancellationToken = default)
        {
            await this.UpdateStatusAsync(messageId, EventState.Published, cancellationToken).ConfigureAwait(false);
        }

        public async Task MarkMessageAsFailedAsync(Guid messageId, CancellationToken cancellationToken = default)
        {
            await this.UpdateStatusAsync(messageId, EventState.PublishedFailed, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Maybe<IReadOnlyList<OutboxMessage>>> GetNotPublishedAsync(Guid transactionId, CancellationToken cancellationToken = default)
        {
            var outboxMessages = await (this.context.OutboxMessages!)
                .Where(e => e.Id == transactionId && e.State == EventState.NotPublished)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return outboxMessages;
        }

        public async Task<Maybe<IReadOnlyList<OutboxMessage>>> GetNotPublishedAsync(CancellationToken cancellationToken = default)
        {
            var outboxMessages = await (this.context.OutboxMessages!)
                .Where(e => e.State == EventState.NotPublished || e.State == EventState.PublishedFailed)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return outboxMessages;
        }

        private async Task UpdateStatusAsync(Guid messageId, EventState eventState, CancellationToken cancellationToken = default)
        {
            var message = this.context.OutboxMessages!.Single(x => x.Id == messageId);
            message.State = eventState;

            this.context.OutboxMessages!.Update(message);

            await this.context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private static OutboxMessage GetOutboxMessage(IIntegrationEvent integrationEvent, Guid transactionId)
        {
            var type = integrationEvent.GetType().FullName ??
                       throw new InvalidOperationException("The type of the message cannot be null.");

            var data = JsonSerializer.Serialize(integrationEvent, integrationEvent.GetType());
            var outboxMessage = new OutboxMessage(transactionId, DateTime.Now, type, data);

            return outboxMessage;
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}