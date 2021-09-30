using System;
using QuizDesigner.Common.Errors;

namespace QuizTopics.Candidate.Domain.Exams
{
    public static class ExamErrors
    {
        public static Error UserAlreadyTokeExam(string user, string exam) =>
            new("user.already.examined", $"User: {user} already toke the exam: {exam}");

        public static Error QuestionDoesNotExists(Guid id) => 
            new("question.not.exists", $"Question with id: {id} does not exists");

        public static Error AnswerDoesNotExists(Guid id) => 
            new("answer.not.exists", $"Answer with id: {id} does not exists");

        public static Error ExamNotFinishedYet(Guid id) => new("exam.not.finished", $"Exam with id: {id} is not finished yet");
    }
}