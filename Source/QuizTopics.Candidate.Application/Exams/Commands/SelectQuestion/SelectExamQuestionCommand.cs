using System;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.Optional;
using QuizDesigner.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Commands.SelectQuestion
{
    public sealed record SelectExamQuestionCommand(Guid ExamId) : ICommand<IResultModel<Maybe<ExamQuestionDto>>>;
}