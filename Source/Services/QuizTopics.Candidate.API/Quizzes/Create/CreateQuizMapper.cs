using System;
using System.Linq;
using QuizTopics.Candidate.Application.Quizzes.Commands.Create;
using QuizTopics.Models;

namespace QuizTopics.Candidate.API.Quizzes.Create
{
    public static class CreateQuizMapper
    {
        public static CreateQuizCommand AsCommand(this CreateQuizModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new CreateQuizCommand(
                model.Exam,
                model.Category,
                model.ExamQuestionCollection.Select(x =>
                    new ExamQuestion(
                        x.Text,
                        x.Tag,
                        x.Level,
                        x.ExamAnswerCollection.Select(y =>
                            new ExamAnswer(
                                y.Text,
                                y.IsCorrect)))));
        }
    }
}