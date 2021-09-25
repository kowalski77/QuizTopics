using System;
using System.Collections.Generic;

namespace QuizDesigner.Events
{
    public sealed record QuizCreated(Guid Id, string Exam, string Category, IEnumerable<ExamQuestion> ExamQuestionCollection);

    public sealed record ExamQuestion(string Text, string Tag, int Difficulty, IEnumerable<ExamAnswer> ExamAnswerCollection);

    public sealed record ExamAnswer(string Text, bool IsCorrect);
}