using System.Collections.Generic;

namespace QuizTopics.Models
{
    public sealed record CreateQuizModel(string Exam, string Category, IEnumerable<Question> ExamQuestionCollection);

    public sealed record Question(string Text, string Tag, int Difficulty, IEnumerable<Answer> ExamAnswerCollection);

    public sealed record Answer(string Text, bool IsCorrect);
}