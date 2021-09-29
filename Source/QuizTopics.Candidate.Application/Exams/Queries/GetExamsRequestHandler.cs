using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace QuizTopics.Candidate.Application.Exams.Queries
{
    public sealed class GetExamsRequestHandler : IRequestHandler<GetExamsRequest, IReadOnlyList<ExamDto>>
    {
        private readonly IExamProvider examProvider;

        public GetExamsRequestHandler(IExamProvider examProvider)
        {
            this.examProvider = examProvider ?? throw new ArgumentNullException(nameof(examProvider));
        }

        public async Task<IReadOnlyList<ExamDto>> Handle(GetExamsRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await this.examProvider.GetExamCollectionAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}