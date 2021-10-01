using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.Results;
using QuizTopics.Candidate.Domain.QuizzesAggregate;

namespace QuizTopics.Candidate.Domain.ExamsAggregate
{
    public interface IExamService
    {
        Task<Result<Exam>> CreateExamAsync(Quiz quiz, string userEmail, CancellationToken cancellationToken = default);
    }
}