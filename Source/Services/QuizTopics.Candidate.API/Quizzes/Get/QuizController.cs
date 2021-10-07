using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Candidate.Application.Quizzes.Queries;
using QuizTopics.Common.Api;
using QuizTopics.Models;

namespace QuizTopics.Candidate.API.Quizzes.Get
{
    [Route("api/v1/[controller]")]
    public class QuizController : ApplicationController
    {
        public QuizController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<QuizModel>))]
        public async Task<IActionResult> GetQuizzesCollection()
        {
            var quizDtoCollection = await this.Mediator.Send(new GetQuizCollectionRequest()).ConfigureAwait(false);

            return Ok(quizDtoCollection.AsModelCollection());
        }
    }
}