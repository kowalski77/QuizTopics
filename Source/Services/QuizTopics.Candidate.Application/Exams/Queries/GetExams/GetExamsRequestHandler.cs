using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QuizTopics.Candidate.Domain.ExamsAggregate;

namespace QuizTopics.Candidate.Application.Exams.Queries.GetExams
{
    public sealed class GetExamsRequestHandler : IRequestHandler<GetExamsRequest, IReadOnlyList<ExamDto>>
    {
        private readonly IExamRepository examRepository;

        public GetExamsRequestHandler(IExamRepository examRepository)
        {
            this.examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        }

        public async Task<IReadOnlyList<ExamDto>> Handle(GetExamsRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return await this.examRepository.GetExamCollectionAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}