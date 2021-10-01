#pragma warning disable 8618
using System;
using QuizDesigner.Common.DomainDriven;

namespace QuizTopics.Candidate.Domain.QuizzesAggregate
{
    public sealed class Answer : Entity
    {
        private Answer() { }

        public Answer(string text, bool isCorrect)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text));
            }

            this.Text = text;
            this.IsCorrect = isCorrect;
        }

        public string Text { get; private set; }

        public bool IsCorrect { get; private set; }
    }
}