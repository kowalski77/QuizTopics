using System;
using System.Linq;
using QuizDesigner.Events;
using QuizDesigner.Shared;

namespace QuizCreatedOrUpdatedService.FunctionApp.Services
{
    public static class QuizMapper
    {
        public static QuizModel AsQuizModel(this QuizCreated? quizCreated)
        {
            if (quizCreated == null)
            {
                throw new ArgumentNullException(nameof(quizCreated));
            }

            return new QuizModel(
                quizCreated.Name,
                quizCreated.Exam,
                quizCreated.ExamQuestionCollection.Select(x =>
                    new Question(
                        x.Text,
                        x.Tag,
                        x.Difficulty,
                        x.ExamAnswerCollection.Select(y =>
                            new Answer(
                                y.Text,
                                y.IsCorrect)))));
        }
    }
}