using System;
using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Candidate.Domain.ExamsAggregate;
using QuizTopics.Common.Mediator;
using QuizTopics.Common.ResultModels;
using QuizTopics.Envelopes;

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

            var resultModel = await ResultModel.Init()
                .OnSuccess(async () => await this.GetExamAsync(request.ExamId, cancellationToken).ConfigureAwait(false))
                .OnSuccess(exam => GetQuestion(request.QuestionId, exam))
                .OnSuccess(async question => await this.SelectAnswerAsync(request.AnswerId, question, cancellationToken).ConfigureAwait(false))
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

        private static IResultModel<ExamQuestion> GetQuestion(Guid questionId, Exam exam)
        {
            var maybeQuestion = exam.GetQuestion(questionId);

            return !maybeQuestion.TryGetValue(out var question) ?
                ResultModel.Fail<ExamQuestion>(GeneralErrors.NotFound(questionId)) :
                ResultModel.Ok(question);
        }

        private async Task<IResultModel> SelectAnswerAsync(Guid answerId, ExamQuestion question, CancellationToken cancellationToken)
        {
            var result = question.CanSelectAnswer(answerId);
            if (!result.Success)
            {
                return ResultModel.Fail(result.Error!);
            }

            question.SelectAnswer(answerId);
            question.SetAsAnswered();

            await this.examRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);

            return ResultModel.Ok();
        }
    }
}