﻿#pragma warning disable 8618
using System;
using QuizDesigner.Common.DomainDriven;

namespace QuizTopics.Candidate.Domain.Exams
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
    }
}