using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizDesigner.Shared;
using QuizTopics.Candidate.Application.Exams.Commands;

namespace QuizTopics.Candidate.API.Exams.Create
{
    [ApiController, Route("api/v1/[controller]"), Authorize]
    [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
    public class ExamController : ControllerBase
    {
        private readonly IMediator mediator;

        public ExamController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<ActionResult<ExamDto>> CreateExam([FromBody] CreateExamModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var response = await this.mediator.Send(model.AsCommand()).ConfigureAwait(false);
            if (response.Success)
            {
                return this.Ok(response.Value.GetValue());
            }

            return this.BadRequest(response.Error);
        }
    }
}