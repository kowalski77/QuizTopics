using System;
using QuizDesigner.Shared;
using QuizTopics.Candidate.Application.Exams.Commands.SelectAnswer;

namespace QuizTopics.Candidate.API.Exams.SelectAnswer
{
    public static class SelectExamAnswerMapper
    {
        public static SelectExamAnswerCommand AsCommand(this SelectExamAnswerModel model, Guid ExamId)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new SelectExamAnswerCommand(ExamId, model.QuestionId, model.AnswerId);
        }
    }
}