using System;
using MediatR;
using QuizDesigner.Common.Optional;
using QuizDesigner.Common.ResultModels;
using QuizTopics.Candidate.Application.Exams.Queries;

namespace QuizTopics.Candidate.Application.Exams.Commands.SelectQuestion
{
    public sealed record SelectExamQuestionCommand(Guid ExamId) : IRequest<IResultModel<Maybe<ExamQuestionDto>>>;
}