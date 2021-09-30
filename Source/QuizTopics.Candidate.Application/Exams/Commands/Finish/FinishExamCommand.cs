using System;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Commands.Finish
{
    public sealed record FinishExamCommand(Guid ExamId) : ICommand<IResultModel>;
}