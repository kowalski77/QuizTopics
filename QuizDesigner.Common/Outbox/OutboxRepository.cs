using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using QuizDesigner.Common.Optional;
using QuizDesigner.Events;

namespace QuizDesigner.Common.Outbox
{
    public class OutboxRepository : IOutboxRepository
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
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            await this.context.Database.UseTransactionAsync(transaction.GetDbTransaction(), cancellationToken);

            var outboxMessage = GetOutboxMessage(integrationEvent, transaction.TransactionId);
            await this.context.AddAsync(outboxMessage, cancellationToken);

            await this.context.SaveEntitiesAsync(cancellationToken);
        }

        public async Task MarkMessageAsInProgressAsync(Guid messageId, CancellationToken cancellationToken = default)
        {
            await this.UpdateStatusAsync(messageId, EventState.InProgress, cancellationToken);
        }

        public async Task MarkMessageAsPublishedAsync(Guid messageId, CancellationToken cancellationToken = default)
        {
            await this.UpdateStatusAsync(messageId, EventState.Published, cancellationToken);
        }

        public async Task MarkMessageAsFailedAsync(Guid messageId, CancellationToken cancellationToken = default)
        {
            await this.UpdateStatusAsync(messageId, EventState.PublishedFailed, cancellationToken);
        }

        public async Task<Maybe<IReadOnlyList<OutboxMessage>>> GetNotPublishedAsync(Guid transactionId, CancellationToken cancellationToken = default)
        {
            var outboxMessages = await (this.context.OutboxMessages!)
                .Where(e => e.Id == transactionId && e.State == EventState.NotPublished)
                .ToListAsync(cancellationToken);

            return outboxMessages;
        }

        public async Task<Maybe<IReadOnlyList<OutboxMessage>>> GetNotPublishedAsync(CancellationToken cancellationToken = default)
        {
            var outboxMessages = await (this.context.OutboxMessages!)
                .Where(e => e.State == EventState.NotPublished || e.State == EventState.PublishedFailed)
                .ToListAsync(cancellationToken);

            return outboxMessages;
        }

        private async Task UpdateStatusAsync(Guid messageId, EventState eventState, CancellationToken cancellationToken = default)
        {
            var message = this.context.OutboxMessages!.Single(x => x.Id == messageId);
            message.State = eventState;

            this.context.OutboxMessages!.Update(message);

            await this.context.SaveChangesAsync(cancellationToken);
        }

        private static OutboxMessage GetOutboxMessage(IIntegrationEvent integrationEvent, Guid transactionId)
        {
            var type = integrationEvent.GetType().FullName ??
                       throw new InvalidOperationException("The type of the message cannot be null.");

            var data = JsonConvert.SerializeObject(integrationEvent);
            var outboxMessage = new OutboxMessage(transactionId, DateTime.Now, type, data);

            return outboxMessage;
        }
    }
}