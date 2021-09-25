using System.Collections.Generic;
using MediatR;

namespace QuizTopics.Candidate.Application.Quizzes.Commands.Queries
{
    public class GetQuizCollectionRequest : IRequest<IReadOnlyList<QuizDto>>
    {
    }
}