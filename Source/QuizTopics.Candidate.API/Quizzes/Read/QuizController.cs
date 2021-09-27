using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Candidate.Application.Quizzes.Queries;

namespace QuizTopics.Candidate.API.Quizzes.Read
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class QuizController : ControllerBase
    {
        private readonly IMediator mediator;

        public QuizController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetQuizCollection()
        {
            var quizDtoCollection = await this.mediator.Send(new GetQuizCollectionRequest()).ConfigureAwait(false);

            return this.Ok(quizDtoCollection);
        }
    }
}