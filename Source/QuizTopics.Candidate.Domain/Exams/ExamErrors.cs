using System;
using QuizDesigner.Common.Errors;

namespace QuizTopics.Candidate.Domain.Exams
{
    public static class ExamErrors
    {
        public static Error UserAlreadyTokeExam(string user, string exam) =>
            new("user.already.examined", $"User: {user} already toke the exam: {exam}");

        public static Error ExamDoesNotExists(Guid id) => 
            new("exam.not.exists", $"Exam with id: {id} does not exists");

        public static Error AnswerDoesNotExists(Guid id) => 
            new("answer.not.exists", $"Answer with id: {id} does not exists");

        public static Error QuestionAlreadyAnswered(Guid id) =>
            new("question.already.answered", $"Question with id: {id} already answered");
    }
}