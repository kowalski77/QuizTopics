using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using QuizTopics.Candidate.Domain.ExamsAggregate.DomainEvents;

namespace QuizTopics.Candidate.Application.Exams.DomainEvents
{
    public sealed class ExamFinishedDomainEventHandler : INotificationHandler<ExamFinishedDomainEvent>
    {
        private readonly ILogger<ExamFinishedDomainEventHandler> logger;

        public ExamFinishedDomainEventHandler(ILogger<ExamFinishedDomainEventHandler> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Handle(ExamFinishedDomainEvent notification, CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"{nameof(ExamFinishedDomainEvent)} received, data: {notification}");

            return Task.CompletedTask;
        }
    }
}