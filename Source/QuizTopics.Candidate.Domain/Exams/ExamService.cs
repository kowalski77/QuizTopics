using System;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.Results;
using QuizTopics.Candidate.Domain.Quizzes;

namespace QuizTopics.Candidate.Domain.Exams
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository examRepository;

        public ExamService(IExamRepository examRepository)
        {
            this.examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        }

        public async Task<Result<Exam>> CreateExamAsync(Quiz quiz, string userEmail, CancellationToken cancellationToken = default)
        {
            if (quiz == null)
            {
                throw new ArgumentNullException(nameof(quiz));
            }

            var maybeExam = await this.examRepository.GetExamByQuizAndCandidate(quiz.Id, userEmail, cancellationToken).ConfigureAwait(false);
            if (maybeExam.TryGetValue(out var existingExam))
            {
                return Result.Fail<Exam>(nameof(userEmail), $" this user: {userEmail} already toke the exam: {existingExam.Quiz.ExamName}");
            }

            var exam = new Exam(quiz, userEmail);

            return Result.Ok(exam);
        }
    }
}