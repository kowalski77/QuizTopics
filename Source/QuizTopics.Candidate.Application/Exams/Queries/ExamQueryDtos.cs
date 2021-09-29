using System;

namespace QuizTopics.Candidate.Application.Exams.Queries
{
    public sealed record ExamDto(Guid Id, string Name, string Candidate);
}