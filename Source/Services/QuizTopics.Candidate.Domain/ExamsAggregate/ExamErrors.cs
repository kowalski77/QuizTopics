using System;
using QuizTopics.Common.Envelopes;

namespace QuizTopics.Candidate.Domain.ExamsAggregate
{
    public static class ExamErrors
    {
        public static ErrorResult UserAlreadyTokeExam(string user, string exam) =>
            new("user.already.examined", $"User: {user} already toke the exam: {exam}");

        public static ErrorResult ExamAlreadyFinished(Guid id) => 
            new("exam.already.finished", $"Exam with id: {id} already finished");

        public static ErrorResult AnswerDoesNotExists(Guid id) => 
            new("answer.not.exists", $"Answer with id: {id} does not exists");

        public static ErrorResult QuestionAlreadyAnswered(Guid id) =>
            new("question.already.answered", $"Question with id: {id} already answered");
    }
}