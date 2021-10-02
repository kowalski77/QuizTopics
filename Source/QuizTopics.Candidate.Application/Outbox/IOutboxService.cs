using System;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.Outbox;
using QuizDesigner.Events;

namespace QuizTopics.Candidate.Application.Outbox
{
    public interface IOutboxService
    {
        Task AddEventAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
        
        Task PublishTransactionEventsAsync(Guid transactionId, CancellationToken cancellationToken = default);

        Task PublishPendingEventsAsync(CancellationToken cancellationToken = default);
    }
}