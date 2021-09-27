using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizDesigner.Common.Api;
using QuizTopics.Candidate.Application.Quizzes.Queries;

namespace QuizTopics.Candidate.API.Quizzes.Read
{
    [Route("api/v1/[controller]"), Authorize]
    [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
    public class QuizController : ApplicationController
    {
        private readonly IMediator mediator;

        public QuizController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QuizDto>))]
        public async Task<IActionResult> GetQuizCollection()
        {
            var quizDtoCollection = await this.mediator.Send(new GetQuizCollectionRequest()).ConfigureAwait(false);

            return this.Ok(quizDtoCollection);
        }
    }
}