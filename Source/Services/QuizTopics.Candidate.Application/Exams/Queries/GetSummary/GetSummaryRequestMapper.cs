using System;
using System.Collections.Generic;
using System.Linq;
using QuizTopics.Candidate.Domain.ExamsAggregate;

namespace QuizTopics.Candidate.Application.Exams.Queries.GetSummary
{
    public static class GetSummaryRequestMapper
    {
        public static SummaryDto ASummaryDto(this Summary summary)
        {
            if (summary == null)
            {
                throw new ArgumentNullException(nameof(summary));
            }

            var correctQuestionsDtoCollection = Map(summary.CorrectExamQuestions);
            var wrongQuestionsDtoCollection = Map(summary.WrongExamQuestions);

            var summaryDto = new SummaryDto(
                correctQuestionsDtoCollection, 
                wrongQuestionsDtoCollection, 
                summary.IsExamPassed);

            return summaryDto;
        }

        private static IEnumerable<ExamQuestionDto> Map(IEnumerable<ExamQuestion> source)
        {
            return source.Select(x =>
                new ExamQuestionDto(x.Id, x.Text, x.Level.Seconds, x.Answered, x.Answers.Select(y =>
                    new ExamAnswerDto(y.Id, y.Text, y.Selected))));
        }
    }
}