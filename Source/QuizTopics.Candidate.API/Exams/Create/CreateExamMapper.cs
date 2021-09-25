using System;
using QuizDesigner.Shared;
using QuizTopics.Candidate.Application.Exams.Commands.Create;

namespace QuizTopics.Candidate.API.Exams.Create
{
    public static class CreateExamMapper
    {
        public static CreateExamCommand AsCommand(this CreateExamModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new CreateExamCommand(model.UserEmail, model.QuizId);
        }
    }
}