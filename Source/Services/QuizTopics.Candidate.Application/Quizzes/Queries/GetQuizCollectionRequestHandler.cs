using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QuizTopics.Candidate.Domain.QuizzesAggregate;

namespace QuizTopics.Candidate.Application.Quizzes.Queries
{
    public class GetQuizCollectionRequestHandler : IRequestHandler<GetQuizCollectionRequest, IReadOnlyList<QuizDto>>
    {
        private readonly IQuizRepository quizRepository;

        public GetQuizCollectionRequestHandler(IQuizRepository quizRepository)
        {
            this.quizRepository = quizRepository ?? throw new ArgumentNullException(nameof(quizRepository));
        }

        public async Task<IReadOnlyList<QuizDto>> Handle(GetQuizCollectionRequest request, CancellationToken cancellationToken)
        {
            throw new InvalidOperationException("Random exception to test the middleware");

            return (await this.quizRepository.GetQuizCollectionAsync(cancellationToken).ConfigureAwait(false)).ToList();
        }
    }
}