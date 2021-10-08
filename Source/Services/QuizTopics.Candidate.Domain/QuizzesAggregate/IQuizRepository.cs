using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Common.DomainDriven;

namespace QuizTopics.Candidate.Domain.QuizzesAggregate
{
    public interface IQuizRepository : IRepository<Quiz>
    {
        Task<IEnumerable<QuizDto>> GetQuizCollectionAsync(CancellationToken cancellationToken = default);
    }
}