using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Candidate.Domain.ExamsAggregate;
using QuizTopics.Common.Monad;

namespace QuizTopics.Candidate.Application.Exams.Queries
{
    public interface IExamProvider
    {
        Task<IReadOnlyList<ExamDto>> GetExamCollectionAsync(CancellationToken cancellationToken = default);

        Task<Maybe<Exam>> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}