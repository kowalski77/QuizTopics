using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizTopics.Candidate.Domain.QuizzesAggregate;
using QuizTopics.Common.DomainDriven;
using QuizTopics.Common.Optional;

namespace QuizTopics.Candidate.Persistence
{
    public class QuizRepository : BaseRepository<Quiz>
    {
        private readonly QuizTopicsContext context;

        public QuizRepository(QuizTopicsContext context) : base(context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override async Task<Maybe<Quiz>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var quiz = await this.context.Quizzes!
                .Include(x => x.QuestionCollection)
                .ThenInclude(x => x.Answers)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                .ConfigureAwait(false);

            return quiz;
        }
    }
}