using System;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.Results;

namespace QuizTopics.Candidate.Domain.Exams
{
    public interface IExamService
    {
        Task<Result<Exam>> CreateExamAsync(Guid quizId, string userEmail, CancellationToken cancellationToken = default);
    }
}