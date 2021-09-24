using System;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.DomainDriven;
using QuizDesigner.Common.Results;
using QuizTopics.Candidate.Domain.Quizzes;

namespace QuizTopics.Candidate.Domain.Exams
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository examRepository;
        private readonly IRepository<Quiz> quizRepository;

        public ExamService(IExamRepository examRepository, IRepository<Quiz> quizRepository)
        {
            this.examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
            this.quizRepository = quizRepository ?? throw new ArgumentNullException(nameof(quizRepository));
        }

        public async Task<Result<Exam>> CreateExamAsync(Guid quizId, string userEmail, CancellationToken cancellationToken = default)
        {
            var existingExam = await this.examRepository.GetExamByQuizAndCandidate(quizId, userEmail, cancellationToken).ConfigureAwait(false);
            if (existingExam.TryGetValue(out _))
            {
                return Result.Fail<Exam>(nameof(userEmail), " this user already toke the exam");
            }

            var maybeQuiz = await this.quizRepository.GetAsync(quizId, cancellationToken).ConfigureAwait(false);
            if (!maybeQuiz.TryGetValue(out var quiz))
            {
                return Result.Fail<Exam>(nameof(quizId), " error obtaining the quiz");
            }

            var exam = new Exam(quiz, userEmail);
            return Result.Ok(exam);
        }
    }
}