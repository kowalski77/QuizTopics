using System.Collections.Generic;
using MediatR;
using QuizTopics.Candidate.Domain.QuizzesAggregate;

namespace QuizTopics.Candidate.Application.Quizzes.Queries
{
    public sealed record GetQuizCollectionRequest : IRequest<IReadOnlyList<QuizDto>>;
}