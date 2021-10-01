using System;
using MediatR;

namespace QuizTopics.Candidate.Domain.ExamsAggregate.DomainEvents
{
    public sealed record ExamFinishedDomainEvent(Summary Summary, DateTime CreatedAt) : INotification;
}