using System;
using QuizTopics.Models;

namespace QuizTopics.Candidate.Wasm.ViewModels
{
    public class ExamViewModel
    {
        public Guid Id { get; private init; }

        public static explicit operator ExamViewModel(ExamModel model)
        {
            return new ExamViewModel
            {
                Id = model.Id
            };
        }
    }
}