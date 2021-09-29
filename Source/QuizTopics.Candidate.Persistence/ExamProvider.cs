using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizTopics.Candidate.Application.Exams.Queries;

namespace QuizTopics.Candidate.Persistence
{
    public class ExamProvider : IExamProvider
    {
        private readonly QuizTopicsContext context;

        public ExamProvider(QuizTopicsContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));

            this.context.ChangeTracker.AutoDetectChangesEnabled = false;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<IReadOnlyList<ExamDto>> GetExamCollectionAsync(CancellationToken cancellationToken = default)
        {
            return await this.context.Exams!
                .Select(x => new ExamDto(x.Id, x.QuizName, x.Candidate))
                .ToListAsync(cancellationToken);
        }
    }
}