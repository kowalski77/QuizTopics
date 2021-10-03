#pragma warning disable 8618
using System;
using System.Collections.Generic;
using System.Linq;
using QuizTopics.Candidate.Domain.ExamsAggregate.DomainEvents;
using QuizTopics.Common.DomainDriven;
using QuizTopics.Common.Monad;
using QuizTopics.Common.Results;

namespace QuizTopics.Candidate.Domain.ExamsAggregate
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

        public Summary Summary => new(this);

        public Maybe<ExamQuestion> GetFirstAvailableQuestion()
        {
            return this.questionsCollection.FirstOrDefault(x => !x.Answered)!;
        }

        public Maybe<ExamQuestion> GetQuestion(Guid id)
        {
            return this.questionsCollection.FirstOrDefault(x => x.Id == id)!;
        }

        public Result CanFinish()
        {
            return this.FinishedAt == null ? 
                Result.Ok() : 
                Result.Fail(ExamErrors.ExamAlreadyFinished(this.Id));
        }

        public void Finish(DateTime finishedAt)
        {
            var result = this.CanFinish();
            if (result.Failure)
            {
                throw new InvalidOperationException(result.Error?.Message);
            }

            this.FinishedAt = finishedAt;
            
            this.AddDomainEvent(new ExamFinishedDomainEvent(this.Summary, finishedAt));
        }
    }
}