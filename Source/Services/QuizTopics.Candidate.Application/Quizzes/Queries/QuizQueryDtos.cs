using System;

namespace QuizTopics.Candidate.Application.Quizzes.Queries
{
    public sealed record QuizDto(Guid Id, string Name, string Category);
}