using System;
using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Candidate.Domain.ExamsAggregate;
using QuizTopics.Common.Envelopes;
using QuizTopics.Common.Mediator;
using QuizTopics.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Queries.GetSummary
{
    public class GetSummaryRequestHandler : ICommandHandler<GetSummaryRequest, IResultModel<SummaryDto>>
    {
        private readonly IExamRepository examRepository;

        public GetSummaryRequestHandler(IExamRepository examRepository)
        {
            this.examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        }

        public async Task<IResultModel<SummaryDto>> Handle(GetSummaryRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var maybeExam = await this.examRepository.GetAsync(request.ExamId, cancellationToken).ConfigureAwait(false);
            if (!maybeExam.TryGetValue(out var exam))
            {
                return ResultModel.Fail<SummaryDto>(GeneralErrors.NotFound(request.ExamId));
            }

            var summaryDto = exam.Summary.ASummaryDto();

            return ResultModel.Ok(summaryDto);
        }
    }
}