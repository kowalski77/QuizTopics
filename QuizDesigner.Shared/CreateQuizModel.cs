using System.Collections.Generic;

namespace QuizDesigner.Shared
{
    public sealed record QuizModel(string Name, string Exam, IEnumerable<Question> ExamQuestionCollection);

    public sealed record Question(string Text, string Tag, int Difficulty, IEnumerable<Answer> ExamAnswerCollection);

    public sealed record Answer(string Text, bool IsCorrect);
}