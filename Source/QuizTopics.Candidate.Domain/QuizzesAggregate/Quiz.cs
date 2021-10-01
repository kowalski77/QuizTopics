#pragma warning disable 8618
using System;
using System.Collections.Generic;
using System.Linq;
using QuizDesigner.Common.DomainDriven;

namespace QuizTopics.Candidate.Domain.QuizzesAggregate
{
    public sealed class Quiz : Entity, IAggregateRoot
    {
        private readonly List<Question> questions = new();

        private Quiz() { }

        public Quiz(string name, string category, IEnumerable<Question> questions)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrEmpty(category))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (questions == null)
            {
                throw new ArgumentNullException(nameof(questions));
            }

            this.Name = name;
            this.Category = category;
            this.questions = questions.ToList();
        }

        public string Name { get; private set; }

        public string Category { get; private set; }

        public IReadOnlyList<Question> QuestionCollection => this.questions;
    }
}