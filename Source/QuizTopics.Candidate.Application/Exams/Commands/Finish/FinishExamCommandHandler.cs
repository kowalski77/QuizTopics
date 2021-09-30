using System;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.Errors;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.ResultModels;
using QuizTopics.Candidate.Domain.Exams;

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
                .OnSuccess(exam => exam.Finish(DateTime.UtcNow))
                .OnSuccess(async () => await this.examRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false))
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
    }
}