using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizDesigner.Common.Optional;
using QuizTopics.Candidate.Application.Exams;
using QuizTopics.Candidate.Application.Exams.Queries;
using QuizTopics.Candidate.Domain.ExamsAggregate;

namespace QuizTopics.Candidate.Persistence
{
    public class ExamProvider : IExamProvider
    {
        private readonly QuizTopicsContext context;

        public ExamProvider(QuizTopicsContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IReadOnlyList<ExamDto>> GetExamCollectionAsync(CancellationToken cancellationToken = default)
        {
            this.context.ChangeTracker.AutoDetectChangesEnabled = false;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await this.context.Exams!
                .Select(x => new ExamDto(x.Id, x.QuizName, x.Candidate))
                .ToListAsync(cancellationToken);
        }

        public async Task<Maybe<Exam>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var exam = await this.context.Exams!
                .Include(x => x.QuestionsCollection)
                .ThenInclude(x => x.Answers)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                .ConfigureAwait(false);

            return exam;
        }
    }
}