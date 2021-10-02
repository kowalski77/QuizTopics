using System;

namespace QuizTopics.Shared
{
    public sealed record CreateExamModel(string UserEmail, Guid QuizId);

    public sealed record SelectExamAnswerModel(Guid QuestionId, Guid AnswerId);

    public sealed record SetFailedExamQuestionModel(Guid QuestionId);
}