using System;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Commands.Create
{
    public sealed record CreateExamCommand(string UserEmail, Guid QuizId) : ICommand<IResultModel<ExamDto>>;
}