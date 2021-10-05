using System;
using QuizTopics.Common.Mediator;

namespace QuizTopics.Candidate.Domain.ExamsAggregate.DomainEvents
{
    public sealed record ExamFinishedDomainEvent(Summary Summary, DateTime CreatedAt) : IDomainNotification;
}