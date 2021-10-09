using System;
using System.Collections.Generic;
using System.Linq;
using QuizTopics.Models;

namespace QuizTopics.Candidate.Wasm.ViewModels
{
    public sealed class ExamQuestionViewModel
    {
        public Guid Id { get; init; }

        public string Text { get; init; }

        public int Difficulty { get; init; }

        public IEnumerable<ExamAnswerViewModel> ExamAnswerViewModelsCollection { get; init; }

        public static explicit operator ExamQuestionViewModel(ExamQuestionModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new ExamQuestionViewModel
            {
                Id = model.Id,
                Text = model.Text,
                Difficulty = model.Difficulty,
                ExamAnswerViewModelsCollection = model.ExamAnswerModelsCollection.Select(x => 
                    new ExamAnswerViewModel
                    {
                        Id = x.Id,
                        Text = x.Text
                    })
            };
        }
    }
}