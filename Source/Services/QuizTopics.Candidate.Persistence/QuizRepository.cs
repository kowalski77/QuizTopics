using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizTopics.Candidate.Domain.QuizzesAggregate;
using QuizTopics.Common.DomainDriven;
using QuizTopics.Common.Monad;

namespace QuizTopics.Candidate.Persistence
{
    public class QuizRepository : BaseRepository<Quiz>, IQuizRepository
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

        public async Task<IEnumerable<QuizDto>> GetQuizCollectionAsync(CancellationToken cancellationToken = default)
        {
            this.context.ChangeTracker.AutoDetectChangesEnabled = false;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await this.context.Quizzes!
                .Select(x => new QuizDto(x.Id, x.Name, x.Category))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}