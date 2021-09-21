using System;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.DomainDriven;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.ResultModels;
using QuizTopics.Candidate.Domain;

namespace QuizTopics.Candidate.Application.Quizzes.Create
{
    public class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand>
    {
        private readonly IRepository<Quiz> quizRepository;

        public CreateQuizCommandHandler(IRepository<Quiz> quizRepository)
        {
            this.quizRepository = quizRepository ?? throw new ArgumentNullException(nameof(quizRepository));
        }

        public async Task<IResultModel> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var quiz = request.AsQuiz();
            _ = this.quizRepository.Add(quiz);

            await this.quizRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);

            return ResultModel.Ok();
        }
    }
}