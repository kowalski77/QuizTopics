using System.Collections.Generic;
using MediatR;

namespace QuizTopics.Candidate.Application.Quizzes.Queries
{
    public sealed record GetQuizCollectionRequest : IRequest<IReadOnlyList<QuizDto>>;
}