using System;
using System.Linq;
using QuizTopics.Candidate.Domain.ExamsAggregate;
using QuizTopics.Candidate.Domain.QuizzesAggregate;

namespace QuizTopics.Candidate.Application.Exams.Queries.SelectQuestion
{
    public static class SelectExamQuestionCommandMapper
    {
        public static ExamQuestionDto AsExamQuestionDto(this ExamQuestion examQuestion)
        {
            if (examQuestion == null)
            {
                throw new ArgumentNullException(nameof(examQuestion));
            }

            return new ExamQuestionDto(
                examQuestion.Id, 
                examQuestion.Text, 
                Level.FindById(examQuestion.Level.Id).Seconds, 
                examQuestion.Answered, 
                examQuestion.Answers.Select(x => 
                    new ExamAnswerDto(
                        x.Id, 
                        x.Text,
                        x.Selected)));
        }
    }
}