using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Candidate.Domain.QuizzesAggregate;
using QuizTopics.Common.Results;

namespace QuizTopics.Candidate.Domain.ExamsAggregate
{
    public interface IExamService
    {
        Task<Result<Exam>> CreateExamAsync(Quiz quiz, string userEmail, CancellationToken cancellationToken = default);
    }
}