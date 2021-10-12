using System;
using System.Linq;
using QuizDesigner.Events;
using QuizTopics.Models;

namespace QuizCreatedOrUpdatedService.FunctionApp.Services
{
    public static class QuizMappers
    {
        public static CreateQuizModel AsModel(this QuizCreated source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new CreateQuizModel(
                source.Exam,
                source.Category,
                source.ExamQuestionCollection.Select(x => new Question(
                    x.Text,
                    x.Tag,
                    x.Difficulty,
                    x.ExamAnswerCollection.Select(y => new Answer(y.Text, y.IsCorrect)))));
        }
    }
}