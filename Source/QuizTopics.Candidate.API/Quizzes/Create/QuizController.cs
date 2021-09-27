using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizDesigner.Shared;

namespace QuizTopics.Candidate.API.Quizzes.Create
{
    //TODO: use gRPC for internal calls
    [ApiController, Route("api/v1/[controller]"), Authorize]
    [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
    public class QuizController : ControllerBase
    {
        private readonly IMediator mediator;

        public QuizController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var response = await this.mediator.Send(model.AsCommand()).ConfigureAwait(false);
            if (response.Success)
            {
                return this.NoContent();
            }

            return this.BadRequest(response.Error);
        }
    }
}