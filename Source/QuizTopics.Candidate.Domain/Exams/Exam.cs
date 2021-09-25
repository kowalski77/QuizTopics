#pragma warning disable 8618
using System;
using System.Collections.Generic;
using System.Linq;
using QuizDesigner.Common.DomainDriven;

namespace QuizTopics.Candidate.Domain.Exams
{
    public class Exam : Entity, IAggregateRoot
    {
        private readonly List<ExamQuestion> questions;

        private Exam() { }

        public Exam(string quizName, string candidate, IEnumerable<ExamQuestion> questions)
        {
            if (string.IsNullOrEmpty(quizName))
            {
                throw new ArgumentNullException(nameof(quizName));
            }

            this.QuizName = quizName;
            this.questions = questions.ToList() ?? throw new ArgumentNullException(nameof(questions));
            this.Candidate = candidate;
        }

        public string QuizName { get; private set; }

        public string Candidate { get; private set; }

        public IReadOnlyList<ExamQuestion> QuestionsCollection => this.questions;
    }
}