using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace QuizTopics.Candidate.Application.Quizzes.Queries
{
    public class GetQuizCollectionRequestHandler : IRequestHandler<GetQuizCollectionRequest, IReadOnlyList<QuizDto>>
    {
        private readonly IQuizProvider quizProvider;

        public GetQuizCollectionRequestHandler(IQuizProvider quizProvider)
        {
            this.quizProvider = quizProvider ?? throw new ArgumentNullException(nameof(quizProvider));
        }

        public async Task<IReadOnlyList<QuizDto>> Handle(GetQuizCollectionRequest request, CancellationToken cancellationToken)
        {
            return (await this.quizProvider.GetQuizCollectionAsync(cancellationToken).ConfigureAwait(false)).ToList();
        }
    }
}