using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Common.Api;
using QuizTopics.Models;

namespace QuizTopics.Candidate.API.Quizzes.Create
{
    //TODO: use gRPC for internal calls
    [Route("api/v1/[controller]")]
    public class QuizController : ApplicationController
    {
        public QuizController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var response = await this.Mediator.Send(model.AsCommand()).ConfigureAwait(false);

            return response.Success ? this.NoContent() : Error(response.ErrorResult, string.Empty);
        }
    }
}