using System;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.Errors;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Queries.GetSummary
{
    public class GetSummaryRequestHandler : ICommandHandler<GetSummaryRequest, IResultModel<SummaryDto>>
    {
        private readonly IExamProvider examProvider;

        public GetSummaryRequestHandler(IExamProvider examProvider)
        {
            this.examProvider = examProvider ?? throw new ArgumentNullException(nameof(examProvider));
        }

        public async Task<IResultModel<SummaryDto>> Handle(GetSummaryRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var maybeExam = await this.examProvider.GetAsync(request.ExamId, cancellationToken).ConfigureAwait(false);
            if (!maybeExam.TryGetValue(out var exam))
            {
                return ResultModel.Fail<SummaryDto>(GeneralErrors.NotFound(request.ExamId));
            }

            var summaryDto = exam.Summary.ASummaryDto();

            return ResultModel.Ok(summaryDto);
        }
    }
}