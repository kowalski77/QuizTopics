using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Common.DomainDriven;
using QuizTopics.Common.Monad;

namespace QuizTopics.Candidate.Domain.ExamsAggregate
{
    public interface IExamRepository : IRepository<Exam>
    {
        Task<Maybe<Exam>> GetExamByCandidateAndQuiz(string candidate, Guid quizId, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ExamDto>> GetExamCollectionAsync(CancellationToken cancellationToken = default);
    }
}