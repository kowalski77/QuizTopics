using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.Optional;
using QuizTopics.Candidate.Domain.Exams;

namespace QuizTopics.Candidate.Application.Exams.Queries
{
    public interface IExamProvider
    {
        Task<IReadOnlyList<ExamDto>> GetExamCollectionAsync(CancellationToken cancellationToken = default);

        Task<Maybe<Exam>> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}