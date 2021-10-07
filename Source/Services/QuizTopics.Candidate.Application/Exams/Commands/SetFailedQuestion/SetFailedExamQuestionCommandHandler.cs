using System;
using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Candidate.Domain.ExamsAggregate;
using QuizTopics.Common.Mediator;
using QuizTopics.Common.ResultModels;
using QuizTopics.Envelopes;

namespace QuizTopics.Candidate.Application.Exams.Commands.SetFailedQuestion
{
    public class SetFailedExamQuestionCommandHandler : ICommandHandler<SetFailedExamQuestionCommand, IResultModel>
    {
        private readonly IExamRepository examRepository;

        public SetFailedExamQuestionCommandHandler(IExamRepository examRepository)
        {
            this.examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        }

        public async Task<IResultModel> Handle(SetFailedExamQuestionCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var resultModel = await ResultModel.Init()
                .OnSuccess(async () => await this.GetExamAsync(request.ExamId, cancellationToken).ConfigureAwait(false))
                .OnSuccess(exam => GetQuestion(request.QuestionId, exam))
                .OnSuccess(async question => await this.SetQuestionAsFailedAsync(question, cancellationToken).ConfigureAwait(false))
                .ConfigureAwait(false);

            return resultModel;
        }

        private async Task<IResultModel> SetQuestionAsFailedAsync(ExamQuestion question, CancellationToken cancellationToken)
        {
            question.SetAsAnswered();
            await this.examRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);

            return ResultModel.Ok();
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
    }
}