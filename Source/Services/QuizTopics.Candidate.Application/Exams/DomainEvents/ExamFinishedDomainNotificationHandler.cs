using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuizTopics.Candidate.Application.Outbox;
using QuizTopics.Candidate.Domain.ExamsAggregate.DomainEvents;
using QuizTopics.Common.Mediator;

namespace QuizTopics.Candidate.Application.Exams.DomainEvents
{
    public sealed class ExamFinishedDomainNotificationHandler : IDomainNotificationHandler<ExamFinishedDomainEvent>
    {
        private readonly ILogger<ExamFinishedDomainNotificationHandler> logger;
        private readonly IOutboxService outboxService;

        public ExamFinishedDomainNotificationHandler(ILogger<ExamFinishedDomainNotificationHandler> logger, IOutboxService outboxService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.outboxService = outboxService ?? throw new ArgumentNullException(nameof(outboxService));
        }

        public async Task Handle(ExamFinishedDomainEvent notification, CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"{nameof(ExamFinishedDomainEvent)} received, data: {notification}");

            var examFinishedIntegrationEvent = notification.AsIntegrationEvent();

            await this.outboxService.AddEventAsync(examFinishedIntegrationEvent, cancellationToken).ConfigureAwait(false);
        }
    }
}