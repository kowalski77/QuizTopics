using System;
using System.Collections.Generic;
using System.Linq;
using QuizTopics.Models;

namespace QuizTopics.Candidate.Wasm.ViewModels
{
    public sealed class ExamQuestionViewModel
    {
        public Guid Id { get; private init; }

        public string Text { get; private init; }

        public int Difficulty { get; private init; }

        public IEnumerable<ExamAnswerViewModel> ExamAnswerViewModelsCollection { get; private init; }

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
                Difficulty = model.CountdownSeconds,
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