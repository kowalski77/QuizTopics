using System;
using QuizTopics.Common.Mediator;
using QuizTopics.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Queries.GetSummary
{
    public sealed record GetSummaryRequest(Guid ExamId) : ICommand<IResultModel<SummaryDto>>;
}