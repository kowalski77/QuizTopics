using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizDesigner.Common.Api;
using QuizDesigner.Shared;

namespace QuizTopics.Candidate.API.Exams.Create
{
    [Route("api/v1/[controller]"), Authorize]
    [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
    public class ExamController : ApplicationController
    {
        private readonly IMediator mediator;

        public ExamController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] CreateExamModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var response = await this.mediator.Send(model.AsCommand()).ConfigureAwait(false);

            return response.Success ? 
                this.Ok(response.Value.GetValue()) : 
                this.Error(response.Error);
        }
    }
}