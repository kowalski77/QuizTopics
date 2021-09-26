#pragma warning disable 8618
using System;
using System.Collections.Generic;
using System.Linq;
using QuizDesigner.Common.DomainDriven;
using QuizDesigner.Common.Optional;

namespace QuizTopics.Candidate.Domain.Exams
{
    public class Exam : Entity, IAggregateRoot
    {
        private readonly List<ExamQuestion> questions;

        private Exam() { }

        public Exam(string quizName, string candidate, DateTime createdAt, IEnumerable<ExamQuestion> questions)
        {
            if (string.IsNullOrEmpty(quizName))
            {
                throw new ArgumentNullException(nameof(quizName));
            }

            this.QuizName = quizName;
            this.Candidate = candidate;
            this.CreatedAt = createdAt;
            this.questions = questions.ToList() ?? throw new ArgumentNullException(nameof(questions));
        }

        public string QuizName { get; private set; }

        public string Candidate { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public IReadOnlyList<ExamQuestion> QuestionsCollection => this.questions;

        public Maybe<ExamQuestion> GetFirstAvailableExamQuestion()
        {
            return this.questions.FirstOrDefault(x => x.Answered)!;
        }
    }
}