using System;
using QuizTopics.Candidate.Application.Exams.Commands.SetFailedQuestion;
using QuizTopics.Models;

namespace QuizTopics.Candidate.API.Exams.SetFailedQuestion
{
    public static class SetFailedExamQuestionMapper
    {
        public static SetFailedExamQuestionCommand AsCommand(this SetFailedExamQuestionModel model, Guid examId)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new SetFailedExamQuestionCommand(examId, model.QuestionId);
        }
    }
}