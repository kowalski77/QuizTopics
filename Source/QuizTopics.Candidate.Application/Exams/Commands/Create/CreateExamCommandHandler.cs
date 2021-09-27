using System;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.DomainDriven;
using QuizDesigner.Common.Errors;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.Optional;
using QuizDesigner.Common.ResultModels;
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
                return ResultModel.Fail(Maybe<ExamDto>.None, GeneralErrors.NotFound(request.QuizId));
            }

            var result = await this.examService.CreateExamAsync(quiz, request.UserEmail, cancellationToken).ConfigureAwait(false);
            if (!result.Success)
            {
                return ResultModel.Fail(Maybe<ExamDto>.None, result.Error!);
            }

            var exam = this.examRepository.Add(result.Value);
            await this.examRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);

            return ResultModel.Ok((Maybe<ExamDto>)exam.AsExamDto());
        }
    }
}