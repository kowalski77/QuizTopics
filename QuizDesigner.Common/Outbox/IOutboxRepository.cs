using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using QuizDesigner.Common.Optional;
using QuizDesigner.Events;

namespace QuizDesigner.Common.Outbox
{
    public interface IOutboxRepository
    {
        Task SaveMessageAsync(IIntegrationEvent integrationEvent, IDbContextTransaction transaction, CancellationToken cancellationToken = default);

        Task MarkMessageAsInProgressAsync(Guid messageId, CancellationToken cancellationToken = default);

        Task MarkMessageAsPublishedAsync(Guid messageId, CancellationToken cancellationToken = default);

        Task MarkMessageAsFailedAsync(Guid messageId, CancellationToken cancellationToken = default);

        Task<Maybe<IReadOnlyList<OutboxMessage>>> GetNotPublishedAsync(Guid transactionId, CancellationToken cancellationToken = default);

        Task<Maybe<IReadOnlyList<OutboxMessage>>> GetNotPublishedAsync(CancellationToken cancellationToken = default);
    }
}