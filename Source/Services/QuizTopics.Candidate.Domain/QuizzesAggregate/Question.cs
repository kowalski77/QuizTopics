#pragma warning disable 8618
using System;
using System.Collections.Generic;
using System.Linq;
using QuizTopics.Common.DomainDriven;

namespace QuizTopics.Candidate.Domain.QuizzesAggregate
{
    public sealed class Question : Entity
    {
        private readonly List<Answer> answerCollection = new();

        private Question() { }

        public Question(string text, string tag, Level level, IEnumerable<Answer> answers)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (string.IsNullOrEmpty(tag))
            {
                throw new ArgumentNullException(nameof(tag));
            }

            this.Text = text;
            this.Tag = tag;
            this.Level = level ?? throw new ArgumentNullException(nameof(level));
            this.answerCollection = answers.ToList() ?? throw new ArgumentNullException(nameof(answers));
        }

        public string Text { get; private set; }

        public string Tag { get; private set; }

        public Level Level { get; }

        public IReadOnlyList<Answer> Answers => this.answerCollection;
    }
}