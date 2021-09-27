using System;
using System.Threading;
using System.Threading.Tasks;
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
                var resultOperation = ResultOperation.Fail(ResultCode.BadRequest, $"Could not find exam with id: {request.ExamId}");

                return ResultModel.Fail(new ExamQuestionDto(), resultOperation);
            }

            var maybeQuestion = exam.GetFirstAvailableExamQuestion();
            var maybeExamQuestionDto = maybeQuestion.Bind<ExamQuestionDto>(examQuestion =>
                examQuestion.AsExamQuestionDto()).ValueOr(new ExamQuestionDto());

            return ResultModel.Ok(maybeExamQuestionDto);
        }
    }
}