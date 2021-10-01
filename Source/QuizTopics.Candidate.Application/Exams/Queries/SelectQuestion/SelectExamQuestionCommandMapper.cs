﻿using System;
using System.Linq;
using QuizTopics.Candidate.Domain.ExamsAggregate;

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
                examQuestion.Difficulty, 
                examQuestion.Answered, 
                examQuestion.Answers.Select(x => 
                    new ExamAnswerDto(
                        x.Id, 
                        x.Text,
                        x.Selected)));
        }
    }
}