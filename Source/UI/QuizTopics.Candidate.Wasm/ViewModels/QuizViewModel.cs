using System;
using QuizTopics.Models;

namespace QuizTopics.Candidate.Wasm.ViewModels
{
    public class QuizViewModel
    {
        public Guid Id { get; private init; }

        public string Name { get; private init; }

        public string Category { get; init; }

        public string Description => $"Name: {this.Name} - Category: {this.Category}";

        public static explicit operator QuizViewModel(QuizModel source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new QuizViewModel
            {
                Id = source.Id,
                Name = source.Name,
                Category = source.Category
            };
        }
    }
}