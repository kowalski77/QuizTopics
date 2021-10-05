using System;
using QuizTopics.Common.Mediator;
using QuizTopics.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Commands.Finish
{
    public sealed record FinishExamCommand(Guid ExamId) : ICommand<IResultModel>;
}