using System;
using MediatR;

namespace QuizTopics.Candidate.Domain.ExamsAggregate.DomainEvents
{
    public class FinishedExamDomainEvent : INotification
    {
        public FinishedExamDomainEvent(Summary summary, DateTime createdAt)
        {
            this.Summary = summary ?? throw new ArgumentNullException(nameof(summary));
            this.CreatedAt = createdAt;
        }

        public Summary Summary { get; }

        public DateTime CreatedAt { get; }
    }
}