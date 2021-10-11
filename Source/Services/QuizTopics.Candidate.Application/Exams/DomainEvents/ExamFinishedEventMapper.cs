using System;
using System.Collections.Generic;
using System.Linq;
using QuizDesigner.Events;
using QuizTopics.Candidate.Domain.ExamsAggregate.DomainEvents;
using ExamQuestionDomain = QuizTopics.Candidate.Domain.ExamsAggregate.ExamQuestion;

namespace QuizTopics.Candidate.Application.Exams.DomainEvents
{
    public static class ExamFinishedEventMapper
    {
        public static ExamFinished AsIntegrationEvent(this ExamFinishedDomainEvent source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var examFinished = new ExamFinished(Guid.NewGuid(), new Summary(
                source.Summary.IsExamPassed,
                source.Summary.CorrectExamQuestions.ToExamQuestionCollection(),
                source.Summary.WrongExamQuestions.ToExamQuestionCollection(),
                source.Summary.QuizName,
                source.Summary.Candidate));

            return examFinished;
        }

        private static IEnumerable<ExamQuestion> ToExamQuestionCollection(this IEnumerable<ExamQuestionDomain> source)
        {
            return source.Select(x =>
                new ExamQuestion(x.Text, x.Tag, x.Level.Seconds, x.Answers.Select(y => 
                    new ExamAnswer(y.Text, y.IsCorrect))));
        }
    }
}