using System;
using MediatR;
using QuizTopics.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Queries.CheckExam
{
    public sealed record CheckExamRequest(Guid Id, string UserEmail, Guid QuizId) : IRequest<IResultModel<bool>>;
}