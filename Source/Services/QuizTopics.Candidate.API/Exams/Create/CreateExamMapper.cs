using System;
using System.Diagnostics;
using QuizTopics.Candidate.Application.Exams;
using QuizTopics.Candidate.Application.Exams.Commands.Create;
using QuizTopics.Models;

namespace QuizTopics.Candidate.API.Exams.Create
{
    public static class CreateExamMapper
    {
        public static CreateExamCommand AsCommand(this ExamModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new CreateExamCommand(model.Id, model.User, model.QuizId);
        }

        public static ExamModel AsModel(this CreateExamDto source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new ExamModel(source.Id, source.UserEmail, source.QuizId);
        }
    }
}