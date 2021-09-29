using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QuizTopics.Candidate.Application.Exams.Queries
{
    public interface IExamProvider
    {
        Task<IReadOnlyList<ExamDto>> GetExamCollectionAsync(CancellationToken cancellationToken = default);
    }
}