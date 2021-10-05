﻿using System;
using QuizTopics.Common.Mediator;
using QuizTopics.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Commands.Create
{
    public sealed record CreateExamCommand(string UserEmail, Guid QuizId) : ICommand<IResultModel<CreateExamDto>>;
}