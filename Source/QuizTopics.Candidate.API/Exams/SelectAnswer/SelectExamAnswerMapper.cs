using System;
using QuizTopics.Candidate.Application.Exams.Commands.SelectAnswer;
using QuizTopics.Models;

namespace QuizTopics.Candidate.API.Exams.SelectAnswer
{
    public static class SelectExamAnswerMapper
    {
        public static SelectExamAnswerCommand AsCommand(this SelectExamAnswerModel model, Guid examId)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new SelectExamAnswerCommand(examId, model.QuestionId, model.AnswerId);
        }
    }
}