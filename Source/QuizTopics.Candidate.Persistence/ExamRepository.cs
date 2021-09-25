using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizDesigner.Common.DomainDriven;
using QuizDesigner.Common.Optional;
using QuizTopics.Candidate.Domain.Exams;

namespace QuizTopics.Candidate.Persistence
{
    public class ExamRepository : BaseRepository<Exam>, IExamRepository
    {
        private readonly QuizTopicsContext context;

        public ExamRepository(QuizTopicsContext context) : base(context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Maybe<Exam>> GetExamByQuizAndCandidate(string quizName, string candidate, CancellationToken cancellationToken = default)
        {
            var exam = await this.context.Exams!
                .Include(x=>x.QuizName)
                .FirstOrDefaultAsync(x => x.QuizName == quizName && x.Candidate == candidate, cancellationToken);

            return exam;
        }
    }
}