#pragma warning disable 8618
using System;
using System.Collections.Generic;
using System.Linq;
using QuizDesigner.Common.DomainDriven;
using QuizDesigner.Common.Optional;
using QuizDesigner.Common.Results;
using QuizTopics.Candidate.Domain.Quizzes;

namespace QuizTopics.Candidate.Domain.Exams
{
    public sealed class ExamQuestion : Entity
    {
        private readonly List<ExamAnswer> answersCollection = new();

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
            this.answersCollection = answers.ToList();
        }

        public string Text { get; private set; }

        public string Tag { get; private set; }

        public Difficulty Difficulty { get; private set; }

        public bool Answered { get; private set; }

        public IEnumerable<ExamAnswer> Answers => this.answersCollection;

        public void SetAsAnswered()
        {
            this.Answered = true;
        }

        public Result CanSelectAnswer(Guid answerId)
        {
            var answerExists = this.answersCollection.Any(x => x.Id == answerId);

            var result = (answerExists, this.Answered) switch
            {
                (false, _ ) =>  Result.Fail(ExamErrors.AnswerDoesNotExists(answerId)),
                (_, true) => Result.Fail(ExamErrors.QuestionAlreadyAnswered(this.Id)),
                _ => Result.Ok()
            };

            return result;
        }

        public void SelectAnswer(Guid answerId)
        {
            var result = this.CanSelectAnswer(answerId);
            if (result.Failure)
            {
                throw new InvalidOperationException(result.Error?.Message);
            }

            var answer = this.answersCollection.First(x => x.Id == answerId);
            answer.Select();
        }

        public Maybe<ExamAnswer> GetAnswer(Guid id)
        {
            return this.answersCollection.FirstOrDefault(x => x.Id == id)!;
        }
    }
}