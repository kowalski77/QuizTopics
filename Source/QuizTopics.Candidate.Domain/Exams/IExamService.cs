using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.Results;
using QuizTopics.Candidate.Domain.Quizzes;

namespace QuizTopics.Candidate.Domain.Exams
{
    public interface IExamService
    {
        Task<Result<Exam>> CreateExamAsync(Quiz quiz, string userEmail, CancellationToken cancellationToken = default);
    }
}