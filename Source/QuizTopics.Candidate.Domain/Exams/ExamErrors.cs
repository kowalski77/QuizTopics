using QuizDesigner.Common.Errors;

namespace QuizTopics.Candidate.Domain.Exams
{
    public static class ExamErrors
    {
        public static Error UserAlreadyTokeExam(string user, string exam) =>
            new("user.already.examined", $"User: {user} already toke the exam: {exam}");
    }
}