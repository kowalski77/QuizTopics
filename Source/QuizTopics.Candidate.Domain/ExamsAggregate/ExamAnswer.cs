#pragma warning disable 8618
using System;
using QuizTopics.Common.DomainDriven;

namespace QuizTopics.Candidate.Domain.ExamsAggregate
{
    public sealed class ExamAnswer : Entity
    {
        private ExamAnswer() { }

        public ExamAnswer(string text, bool isCorrect)
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

        public bool Selected { get; private set; }

        public void Select()
        {
            this.Selected = true;
        }
    }
}