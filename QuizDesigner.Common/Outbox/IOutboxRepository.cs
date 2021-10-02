using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using QuizDesigner.Common.Optional;

namespace QuizDesigner.Common.Outbox
{
    public interface IOutboxRepository
    {
        Task SaveMessageAsync(IIntegrationEvent integrationEvent, IDbContextTransaction transaction);

        Task MarkMessageAsInProgressAsync(Guid messageId);

        Task MarkMessageAsPublishedAsync(Guid messageId);

        Task MarkMessageAsFailedAsync(Guid messageId);

        Task<Maybe<IReadOnlyList<OutboxMessage>>> GetNotPublishedAsync(Guid transactionId);

        Task<Maybe<IReadOnlyList<OutboxMessage>>> GetNotPublishedAsync();
    }
}