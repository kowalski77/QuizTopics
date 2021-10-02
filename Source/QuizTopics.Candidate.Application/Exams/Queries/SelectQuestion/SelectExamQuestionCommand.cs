using System;
using MediatR;
using QuizTopics.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Queries.SelectQuestion
{
    public sealed record SelectExamQuestionCommand(Guid ExamId) : IRequest<IResultModel<ExamQuestionDto>>;
}