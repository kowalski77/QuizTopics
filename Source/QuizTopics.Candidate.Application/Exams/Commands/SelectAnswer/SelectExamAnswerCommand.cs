using System;
using QuizDesigner.Common.Mediator;
using QuizDesigner.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Commands.SelectAnswer
{
    public sealed record SelectExamAnswerCommand(Guid ExamId, Guid QuestionId, Guid AnswerId) : ICommand<IResultModel>;
}