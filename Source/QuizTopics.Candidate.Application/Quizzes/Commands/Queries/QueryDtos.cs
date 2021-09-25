using System;

namespace QuizTopics.Candidate.Application.Quizzes.Commands.Queries
{
    public sealed record QuizDto(Guid Id, string Name, string Category);
}