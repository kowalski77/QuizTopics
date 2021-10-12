﻿using System;
using System.Collections.Generic;

[assembly: CLSCompliant(false)]
namespace QuizDesigner.Events
{
    public sealed record QuizCreated(Guid Id, string Exam, string Category, IEnumerable<ExamQuestion> ExamQuestionCollection) : IIntegrationEvent;

    public sealed record ExamQuestion(string Text, string Tag, int Seconds, IEnumerable<ExamAnswer> ExamAnswerCollection);

    public sealed record ExamAnswer(string Text, bool IsCorrect);

    public sealed record ExamFinished(Guid Id, Summary Summary) : IIntegrationEvent;

    public sealed record Summary(
        Guid QuizId,
        bool Passed,
        IEnumerable<string> CorrectQuestionsCollection,
        IEnumerable<string> WrongQuestionsCollection,
        string Candidate);
}