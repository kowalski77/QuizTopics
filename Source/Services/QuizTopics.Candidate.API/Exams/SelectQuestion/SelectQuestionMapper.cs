using System;
using System.Linq;
using QuizTopics.Candidate.Domain.ExamsAggregate;
using QuizTopics.Models;

namespace QuizTopics.Candidate.API.Exams.SelectQuestion
{
    public static class SelectQuestionMapper
    {
        public static ExamQuestionModel AsModel(this ExamQuestionDto source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new ExamQuestionModel(
                source.Id, 
                source.Text, 
                source.Seconds, 
                source.ExamAnswersCollection.Select(x => 
                    new ExamAnswerModel(x.Id, x.Text)));
        }
    }
}