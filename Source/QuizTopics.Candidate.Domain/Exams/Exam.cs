#pragma warning disable 8618
using System;
using System.Linq;
using QuizDesigner.Common.DomainDriven;
using QuizTopics.Candidate.Domain.Quizzes;

namespace QuizTopics.Candidate.Domain.Exams
{
    public class Exam : Entity, IAggregateRoot
    {
        private Exam() { }

        public Exam(Quiz quiz, string candidate)
        {
            this.Quiz = quiz ?? throw new ArgumentNullException(nameof(quiz));
            this.Candidate = candidate;
        }

        public Quiz Quiz { get; private set; }

        public string Candidate { get; private set; }

        public Guid NextQuestionId { get; private set; }

        public void SetNextQuestionId(Guid questionId)
        {
            this.NextQuestionId = questionId;
        }

        public Question GetNextQuestion()
        {
            return this.Quiz.QuestionCollection.First(x => x.Id == this.NextQuestionId);
        }
    }
}