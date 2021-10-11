using System.Collections.Generic;
using QuizTopics.Common.Mediator;
using QuizTopics.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Quizzes.Commands.Create
{
    public sealed record CreateQuizCommand(string Name, string Category, IEnumerable<ExamQuestion> ExamQuestionCollection) : ICommand<IResultModel>;

    public sealed record ExamQuestion(string Text, string Tag, string Level, IEnumerable<ExamAnswer> ExamAnswerCollection);

    public sealed record ExamAnswer(string Text, bool IsCorrect);
}