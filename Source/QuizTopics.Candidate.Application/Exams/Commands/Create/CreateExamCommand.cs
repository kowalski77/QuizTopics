using System;
using QuizDesigner.Common.Mediator;

namespace QuizTopics.Candidate.Application.Exams.Commands.Create
{
    public sealed record CreateExamCommand(string UserEmail, Guid QuizId) : ICommand;
}