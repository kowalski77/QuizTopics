using System.Collections.Generic;
using MediatR;
using QuizTopics.Candidate.Domain.ExamsAggregate;

namespace QuizTopics.Candidate.Application.Exams.Queries.GetExams
{
    public sealed record GetExamsRequest : IRequest<IReadOnlyList<ExamDto>>;
}