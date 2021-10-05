using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizTopics.Candidate.Application.Quizzes.Queries;

namespace QuizTopics.Candidate.Persistence
{
    public sealed class QuizProvider : IQuizProvider
    {
        private readonly QuizTopicsContext context;

        public QuizProvider(QuizTopicsContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));

            this.context.ChangeTracker.AutoDetectChangesEnabled = false;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<IEnumerable<QuizDto>> GetQuizCollectionAsync(CancellationToken cancellationToken = default)
        {
            return await this.context.Quizzes!
                .Select(x => new QuizDto(x.Id, x.Name, x.Category))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}