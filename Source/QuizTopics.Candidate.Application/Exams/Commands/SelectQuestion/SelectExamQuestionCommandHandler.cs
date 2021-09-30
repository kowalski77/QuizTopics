using System;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.Errors;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.ResultModels;
using QuizTopics.Candidate.Domain.Exams;

namespace QuizTopics.Candidate.Application.Exams.Commands.SelectQuestion
{
    // TODO: to queries?
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

            var resultModel = await ResultModel.Init()
                .OnSuccess(async () => await this.GetExamAsync(request.ExamId, cancellationToken).ConfigureAwait(false))
                .OnSuccess(SelectAvailableQuestion)
                .ConfigureAwait(false);

            return resultModel;
        }

        private async Task<IResultModel<Exam>> GetExamAsync(Guid examId, CancellationToken cancellationToken)
        {
            var maybeExam = await this.examRepository.GetAsync(examId, cancellationToken).ConfigureAwait(false);

            return !maybeExam.TryGetValue(out var exam) ? 
                ResultModel.Fail<Exam>(GeneralErrors.NotFound(examId)) : 
                ResultModel.Ok(exam);
        }

        private static IResultModel<ExamQuestionDto> SelectAvailableQuestion(Exam exam)
        {
            var maybeQuestion = exam.GetFirstAvailableQuestion();

            var maybeExamQuestionDto = maybeQuestion.Bind<ExamQuestionDto>(examQuestion =>
                    examQuestion.AsExamQuestionDto())
                .ValueOr(ExamQuestionDto.None);

            return ResultModel.Ok(maybeExamQuestionDto);
        }
    }
}