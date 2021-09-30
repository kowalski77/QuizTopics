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
        private readonly List<ExamQuestion> questionsCollection = new();

        private Exam() { }

        public Exam(string quizName, string candidate, 
            DateTime createdAt, IEnumerable<ExamQuestion> questions)
        {
            if (string.IsNullOrEmpty(quizName))
            {
                throw new ArgumentNullException(nameof(quizName));
            }

            this.QuizName = quizName;
            this.Candidate = candidate;
            this.CreatedAt = createdAt;
            this.questionsCollection = questions.ToList() ?? throw new ArgumentNullException(nameof(questions));
        }

        public string QuizName { get; private set; }

        public string Candidate { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? FinishedAt { get; private set; }

        public IReadOnlyList<ExamQuestion> QuestionsCollection => this.questionsCollection;

        public Maybe<ExamQuestion> GetFirstAvailableQuestion()
        {
            return this.questionsCollection.FirstOrDefault(x => !x.Answered)!;
        }

        public Maybe<ExamQuestion> GetQuestion(Guid id)
        {
            return this.questionsCollection.FirstOrDefault(x => x.Id == id)!;
        }

        public void Finish(DateTime finishedAt)
        {
            this.FinishedAt = finishedAt;
        }
    }
}