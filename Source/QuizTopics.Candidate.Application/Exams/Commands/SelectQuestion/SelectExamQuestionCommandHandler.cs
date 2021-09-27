using System;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.Errors;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.ResultModels;
using QuizTopics.Candidate.Domain.Exams;

namespace QuizTopics.Candidate.Application.Exams.Commands.SelectQuestion
{
    public class SelectExamQuestionCommandHandler : ICommandHandler<SelectExamQuestionCommand, IResultModel<ExamQuestionDto>>
    {
        private readonly IExamRepository examRepository;

        public SelectExamQuestionCommandHandler(IExamRepository examRepository)
        {
            this.examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        }

        public async Task<IResultModel<ExamQuestionDto>> Handle(SelectExamQuestionCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var maybeExam = await this.examRepository.GetAsync(request.ExamId, cancellationToken).ConfigureAwait(false);
            if (!maybeExam.TryGetValue(out var exam))
            {
                return ResultModel.Fail(new ExamQuestionDto(), GeneralErrors.NotFound(request.ExamId));
            }

            var maybeQuestion = exam.GetFirstAvailableQuestion();
            var maybeExamQuestionDto = maybeQuestion.Bind<ExamQuestionDto>(examQuestion =>
                examQuestion.AsExamQuestionDto()).ValueOr(new ExamQuestionDto());

            return ResultModel.Ok(maybeExamQuestionDto);
        }
    }
}