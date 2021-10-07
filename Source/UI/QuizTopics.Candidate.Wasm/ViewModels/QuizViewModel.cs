using System;

namespace QuizTopics.Candidate.Wasm.ViewModels
{
    public class QuizViewModel
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Category { get; init; }

        public string Description => $"Name: {this.Name} - Category: {this.Category}";
    }
}