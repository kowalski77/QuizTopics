using System;

namespace QuizTopics.Candidate.Domain.QuizzesAggregate
{
    public sealed record QuizDto(Guid Id, string Name, string Category);
}