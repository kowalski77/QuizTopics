using System;
using System.Collections.Generic;

namespace QuizTopics.Models
{
    public sealed record QuizModel(Guid Id, string Name, string Category);

    public sealed record CreateQuizModel(Guid QuizId, string Exam, string Category, IEnumerable<Question> ExamQuestionCollection);

    public sealed record Question(string Text, string Tag, int Difficulty, IEnumerable<Answer> ExamAnswerCollection);

    public sealed record Answer(string Text, bool IsCorrect);
}