using System.Collections.Generic;
using MediatR;

namespace QuizTopics.Candidate.Application.Exams.Queries
{
    public sealed record GetExamsRequest : IRequest<IReadOnlyList<ExamDto>>;
}