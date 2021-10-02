using System;
using System.Collections.Generic;

//TODO: common nuget
namespace QuizDesigner.Events
{
    public sealed record QuizCreated(Guid Id, string Exam, string Category, IEnumerable<ExamQuestion> ExamQuestionCollection) : IIntegrationEvent;

    public sealed record ExamQuestion(string Text, string Tag, int Difficulty, IEnumerable<ExamAnswer> ExamAnswerCollection);

    public sealed record ExamAnswer(string Text, bool IsCorrect);

    public sealed record ExamFinished(Guid Id, Summary Summary) : IIntegrationEvent;

    public sealed record Summary(
        bool Passed,
        IEnumerable<ExamQuestion> CorrectQuestionsCollection,
        IEnumerable<ExamQuestion> WrongQuestionsCollection,
        string QuizName,
        string Candidate);
}