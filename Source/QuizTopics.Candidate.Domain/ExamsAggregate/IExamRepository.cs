using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Common.DomainDriven;
using QuizTopics.Common.Optional;

namespace QuizTopics.Candidate.Domain.ExamsAggregate
{
    public interface IExamRepository : IRepository<Exam>
    {
        Task<Maybe<Exam>> GetExamByQuizAndCandidateAsync(string quizName, string candidate, CancellationToken cancellationToken = default);
    }
}