using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizTopics.Candidate.Domain.ExamsAggregate;
using QuizTopics.Common.DomainDriven;
using QuizTopics.Common.Monad;

namespace QuizTopics.Candidate.Persistence
{
    public class ExamRepository : BaseRepository<Exam>, IExamRepository
    {
        private readonly QuizTopicsContext context;

        public ExamRepository(QuizTopicsContext context) : base(context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Maybe<Exam>> GetExamByCandidateAndQuiz(string candidate, Guid quizId, CancellationToken cancellationToken = default)
        {
            var exam = await this.context.Exams!
                .FirstOrDefaultAsync(x => x.QuizId == quizId && x.Candidate == candidate, cancellationToken)
                .ConfigureAwait(false);

            return exam;
        }

        public override async Task<Maybe<Exam>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var exam = await this.context.Exams!
                .Include(x => x.QuestionsCollection)
                .ThenInclude(x => x.Answers)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                .ConfigureAwait(false);

            return exam;
        }

        public async Task<IReadOnlyList<ExamDto>> GetExamCollectionAsync(CancellationToken cancellationToken = default)
        {
            this.context.ChangeTracker.AutoDetectChangesEnabled = false;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await this.context.Exams!
                .Select(x => new ExamDto(x.Id, x.QuizName, x.Candidate))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}