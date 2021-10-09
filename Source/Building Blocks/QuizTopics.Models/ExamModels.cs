using System;
using System.Collections.Generic;

//TODO: consider to remove this  models assembly, it's worthy?
[assembly: CLSCompliant(false)]

namespace QuizTopics.Models
{
    public sealed record ExamModel(Guid Id, string User, Guid QuizId);

    public sealed record SelectExamAnswerModel(Guid QuestionId, Guid AnswerId);

    public sealed record SetFailedExamQuestionModel(Guid QuestionId);

    public sealed record ExamQuestionModel(Guid Id, string Text, int Difficulty, IEnumerable<ExamAnswerModel> ExamAnswerModelsCollection);

    public sealed record ExamAnswerModel(Guid Id, string Text);
}