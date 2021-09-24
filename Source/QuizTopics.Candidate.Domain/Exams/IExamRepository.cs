using System;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.DomainDriven;
using QuizDesigner.Common.Optional;

namespace QuizTopics.Candidate.Domain.Exams
{
    public interface IExamRepository : IRepository<Exam>
    {
        Task<Maybe<Exam>> GetExamByQuizAndCandidate(Guid quizId, string candidate, CancellationToken cancellationToken = default);
    }
}