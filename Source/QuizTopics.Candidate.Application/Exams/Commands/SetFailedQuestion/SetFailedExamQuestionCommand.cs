using System;
using QuizTopics.Common.Mediator;
using QuizTopics.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Commands.SetFailedQuestion
{
    public sealed record SetFailedExamQuestionCommand(Guid ExamId, Guid QuestionId) : ICommand<IResultModel>;
}