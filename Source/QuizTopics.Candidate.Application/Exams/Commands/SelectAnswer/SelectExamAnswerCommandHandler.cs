using System;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.Errors;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.ResultModels;
using QuizTopics.Candidate.Domain.Exams;

namespace QuizTopics.Candidate.Application.Exams.Commands.SelectAnswer
{
    public class SelectExamAnswerCommandHandler : ICommandHandler<SelectExamAnswerCommand, IResultModel>
    {
        private readonly IExamRepository examRepository;

        public SelectExamAnswerCommandHandler(IExamRepository examRepository)
        {
            this.examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        }

        public async Task<IResultModel> Handle(SelectExamAnswerCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var resultModel = ResultModel.Init();

            var maybeExam = await this.examRepository.GetAsync(request.ExamId, cancellationToken).ConfigureAwait(false);
            if (!maybeExam.TryGetValue(out var exam))
            {
                resultModel = ResultModel.Fail(GeneralErrors.NotFound(request.ExamId));
            }

            await resultModel.OnSuccess(() =>
            {
                var maybeQuestion = exam.GetQuestion(request.QuestionId);
                if (!maybeQuestion.TryGetValue(out var question))
                {
                    resultModel = ResultModel.Fail(GeneralErrors.NotFound(request.QuestionId));
                }

                return question;
            })
                .OnSuccess(async question =>
                {
                    var result = question.CanSelectAnswer(request.AnswerId);
                    if (!result.Success)
                    {
                        return ResultModel.Fail(result.Error!);
                    }

                    question.SelectAnswer(request.AnswerId);
                    question.SetAsAnswered();

                    await this.examRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);

                    return ResultModel.Ok();
                })
                .ConfigureAwait(false);

            return resultModel;
        }
    }
}