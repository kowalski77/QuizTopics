using System;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Commands.SetFailedQuestion
{
    public sealed record SetFailedExamQuestionCommand(Guid ExamId, Guid QuestionId) : ICommand<IResultModel>;
}