using System;

namespace QuizDesigner.Shared
{
    public sealed record CreateExamModel(string UserEmail, Guid QuizId);

    public sealed record SelectExamAnswerModel(Guid QuestionId, Guid AnswerId);
}