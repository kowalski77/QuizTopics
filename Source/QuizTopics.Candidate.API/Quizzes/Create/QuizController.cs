using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizDesigner.Shared;
using QuizTopics.Candidate.Application.Quizzes.Create;

namespace QuizTopics.Candidate.API.Quizzes.Create
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class QuizController : ControllerBase
    {
        private readonly IMediator mediator;

        public QuizController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz([FromBody] QuizModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var response = await this.mediator.Send(model.AsCommand()).ConfigureAwait(false);
            if (response.Success)
            {
                return this.Ok();
            }

            return this.BadRequest(response.ResultOperation.ToString());
        }
    }
}