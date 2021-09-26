#pragma warning disable 8618
using System;
using System.Collections.Generic;
using System.Linq;
using QuizDesigner.Common.DomainDriven;
using QuizTopics.Candidate.Domain.Quizzes;

namespace QuizTopics.Candidate.Domain.Exams
{
    public sealed class ExamQuestion : Entity
    {
        private readonly List<ExamAnswer> answerCollection;

        private ExamQuestion() { }

        public ExamQuestion(string text, string tag, Difficulty difficulty, IEnumerable<ExamAnswer> answers)
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

        public bool Answered { get; private set; }

        public IEnumerable<ExamAnswer> Answers => this.answerCollection;

        public void SetAsAnswered()
        {
            this.Answered = true;
        }
    }
}