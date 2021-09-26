using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QuizTopics.Candidate.Application.Quizzes.Queries
{
    public interface IQuizProvider
    {
        Task<IEnumerable<QuizDto>> GetQuizCollectionAsync(CancellationToken cancellationToken = default);
    }
}