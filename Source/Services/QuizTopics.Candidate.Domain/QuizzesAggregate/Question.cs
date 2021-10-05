﻿#pragma warning disable 8618
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

        public Question(string text, string tag, Difficulty difficulty, IEnumerable<Answer> answers)
        {
            if (answers == null)
            {
                throw new ArgumentNullException(nameof(answers));
            }

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
            this.Difficulty = difficulty;
            this.answerCollection = answers.ToList();
        }

        public string Text { get; private set; }

        public string Tag { get; private set; }

        public Difficulty Difficulty { get; private set; }

        public IReadOnlyList<Answer> Answers => this.answerCollection;
    }
}