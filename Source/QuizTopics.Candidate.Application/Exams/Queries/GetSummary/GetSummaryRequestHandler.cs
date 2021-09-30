using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.Errors;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.ResultModels;
using QuizTopics.Candidate.Domain.Exams;

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

            var maybeSummary = await this.examProvider.GetExamAsync(request.ExamId, cancellationToken).ConfigureAwait(false);
            if (!maybeSummary.TryGetValue(out var exam))
            {
                return ResultModel.Fail<SummaryDto>(GeneralErrors.NotFound(request.ExamId));
            }

            var correctQuestionsDtoCollection = Map(exam.GetCorrectQuestions());
            var wrongQuestionsDtoCollection = Map(exam.GetWrongQuestions());

            var summary = new SummaryDto(exam.Id, correctQuestionsDtoCollection, wrongQuestionsDtoCollection, exam.IsExamPassed);

            return ResultModel.Ok(summary);
        }

        private static IEnumerable<ExamQuestionDto> Map(IEnumerable<ExamQuestion> source)
        {
            return source.Select(x =>
                new ExamQuestionDto(x.Id, x.Text, x.Difficulty, x.Answered, x.Answers.Select(y =>
                    new ExamAnswerDto(y.Id, y.Text, y.Selected))));
        }
    }
}