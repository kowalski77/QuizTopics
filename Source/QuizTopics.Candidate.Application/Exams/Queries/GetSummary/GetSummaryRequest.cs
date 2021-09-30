using System;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Queries.GetSummary
{
    public sealed record GetSummaryRequest(Guid ExamId) : ICommand<IResultModel<SummaryDto>>;
}