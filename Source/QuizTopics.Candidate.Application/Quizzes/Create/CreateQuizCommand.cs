using System.Collections.Generic;
using QuizDesigner.Common.Mediator;

namespace QuizTopics.Candidate.Application.Quizzes.Create
{
    public sealed record CreateQuizCommand(string Name, string Category, IEnumerable<ExamQuestion> ExamQuestionCollection) : ICommand;

    public sealed record ExamQuestion(string Text, string Tag, int Difficulty, IEnumerable<ExamAnswer> ExamAnswerCollection);

    public sealed record ExamAnswer(string Text, bool IsCorrect);
}