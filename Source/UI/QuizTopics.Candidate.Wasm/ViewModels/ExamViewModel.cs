using System;
using QuizTopics.Models;

namespace QuizTopics.Candidate.Wasm.ViewModels
{
    public class ExamViewModel
    {
        public string User { get; init; }

        public Guid QuizId { get; init; }

        public static explicit operator ExamViewModel(ExamModel model)
        {
            var (_, user, quizId) = model;
            return new ExamViewModel
            {
                User = user,
                QuizId = quizId
            };
        }
    }
}