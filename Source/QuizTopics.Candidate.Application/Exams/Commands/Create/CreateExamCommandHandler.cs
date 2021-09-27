using System;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.DomainDriven;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.Optional;
using QuizDesigner.Common.ResultModels;
using QuizDesigner.Common.Results;
using QuizTopics.Candidate.Domain.Exams;
using QuizTopics.Candidate.Domain.Quizzes;

namespace QuizTopics.Candidate.Application.Exams.Commands.Create
{
    public class CreateExamCommandHandler : ICommandHandler<CreateExamCommand, IResultModel<Maybe<ExamDto>>>
    {
        private readonly IExamService examService;
        private readonly IRepository<Exam> examRepository;
        private readonly IRepository<Quiz> quizRepository;

        public CreateExamCommandHandler(IExamService examService, IRepository<Exam> examRepository, IRepository<Quiz> quizRepository)
        {
            this.examService = examService ?? throw new ArgumentNullException(nameof(examService));
            this.examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
            this.quizRepository = quizRepository ?? throw new ArgumentNullException(nameof(quizRepository));
        }

        public async Task<IResultModel<Maybe<ExamDto>>> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var maybeQuiz = await this.quizRepository.GetAsync(request.QuizId, cancellationToken).ConfigureAwait(false);
            if (!maybeQuiz.TryGetValue(out var quiz))
            {
                var resultOperation = ResultOperation.Fail(ResultCode.BadRequest, $"Quiz with id: {request.QuizId} not found");

                return ResultModel.Fail(Maybe<ExamDto>.None, resultOperation);
            }

            var result = await this.examService.CreateExamAsync(quiz, request.UserEmail, cancellationToken).ConfigureAwait(false);
            if (!result.Success)
            {
                var resultOperation = ResultOperation.Fail(ResultCode.BadRequest, result.Error);
                return ResultModel.Fail(Maybe<ExamDto>.None, resultOperation);
            }

            var exam = this.examRepository.Add(result.Value);
            await this.examRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);

            return ResultModel.Ok((Maybe<ExamDto>)exam.AsExamDto());
        }
    }
}