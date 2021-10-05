using System;

[assembly: CLSCompliant(false)]

namespace QuizTopics.Models
{
    public sealed record ExamModel(Guid Id, string User, Guid QuizId);

    public sealed record SelectExamAnswerModel(Guid QuestionId, Guid AnswerId);

    public sealed record SetFailedExamQuestionModel(Guid QuestionId);
}