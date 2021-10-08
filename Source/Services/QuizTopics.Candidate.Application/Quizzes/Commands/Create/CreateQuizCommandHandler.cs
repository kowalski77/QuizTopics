using System;
using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Candidate.Domain.QuizzesAggregate;
using QuizTopics.Common.Mediator;
using QuizTopics.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Quizzes.Commands.Create
{
    public class CreateQuizCommandHandler : ICommandHandler<CreateQuizCommand, IResultModel>
    {
        private readonly IQuizRepository quizRepository;

        public CreateQuizCommandHandler(IQuizRepository quizRepository)
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