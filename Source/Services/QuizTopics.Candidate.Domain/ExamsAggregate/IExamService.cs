using System;
using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Candidate.Domain.QuizzesAggregate;
using QuizTopics.Common.Results;

namespace QuizTopics.Candidate.Domain.ExamsAggregate
{
    public interface IExamService
    {
        Task<Result<Exam>> CreateExamAsync(Quiz quiz, Guid id, string userEmail, CancellationToken cancellationToken = default);
    }
}