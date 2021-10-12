using System;
using System.Linq;
using QuizDesigner.Events;
using QuizTopics.Candidate.Domain.ExamsAggregate.DomainEvents;

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
                source.Summary.QuizId,
                source.Summary.IsExamPassed,
                source.Summary.CorrectExamQuestions.Select(x => x.Text),
                source.Summary.WrongExamQuestions.Select(x => x.Text).ToList(),
                source.Summary.Candidate));

            return examFinished;
        }
    }
}