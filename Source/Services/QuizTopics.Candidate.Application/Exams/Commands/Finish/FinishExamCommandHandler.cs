using System;
using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Candidate.Domain.ExamsAggregate;
using QuizTopics.Common.Envelopes;
using QuizTopics.Common.Mediator;
using QuizTopics.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Commands.Finish
{
    public class FinishExamCommandHandler : ICommandHandler<FinishExamCommand, IResultModel>
    {
        private readonly IExamRepository examRepository;

        public FinishExamCommandHandler(IExamRepository examRepository)
        {
            this.examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        }

        public async Task<IResultModel> Handle(FinishExamCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var resultModel = await ResultModel.Init()
                .OnSuccess(async () => await this.GetExamAsync(request.ExamId, cancellationToken).ConfigureAwait(false))
                .OnSuccess(exam => FinishExam(exam, DateTime.UtcNow))
                .OnSuccess(async () => await this.examRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false))
                .ConfigureAwait(false);

            return resultModel;
        }

        private static IResultModel FinishExam(Exam exam, DateTime finishedAt)
        {
            var resultModel = exam.CanFinish();
            if (resultModel.Failure)
            {
                return ResultModel.Fail(resultModel.Error);
            }

            exam.Finish(finishedAt);

            return ResultModel.Ok();
        }

        private async Task<IResultModel<Exam>> GetExamAsync(Guid examId, CancellationToken cancellationToken)
        {
            var maybeExam = await this.examRepository.GetAsync(examId, cancellationToken).ConfigureAwait(false);

            return !maybeExam.TryGetValue(out var exam) ? 
                ResultModel.Fail<Exam>(GeneralErrors.NotFound(examId)) : 
                ResultModel.Ok(exam);
        }
    }
}