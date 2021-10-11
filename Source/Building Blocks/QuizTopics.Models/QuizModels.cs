using System;
using System.Collections.Generic;

namespace QuizTopics.Models
{
    public sealed record QuizModel(Guid Id, string Name, string Category);

    public sealed record CreateQuizModel(string Exam, string Category, IEnumerable<Question> ExamQuestionCollection);

    public sealed record Question(string Text, string Tag, string Level, IEnumerable<Answer> ExamAnswerCollection);

    public sealed record Answer(string Text, bool IsCorrect);
}