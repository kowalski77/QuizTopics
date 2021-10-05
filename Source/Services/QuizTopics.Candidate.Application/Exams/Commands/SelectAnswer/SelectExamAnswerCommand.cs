using System;
using QuizTopics.Common.Mediator;
using QuizTopics.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Commands.SelectAnswer
{
    public sealed record SelectExamAnswerCommand(Guid ExamId, Guid QuestionId, Guid AnswerId) : ICommand<IResultModel>;
}