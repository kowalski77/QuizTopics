using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizDesigner.Common.Api;
using QuizDesigner.Shared;

namespace QuizTopics.Candidate.API.Exams.SelectAnswer
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

        [HttpPost("{examId:guid}/selectExamAnswer")]
        public async Task<IActionResult> SelectExamAnswer(Guid examId, [FromBody] SelectExamAnswerModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var response = await this.mediator.Send(model.AsCommand(examId)).ConfigureAwait(false);

            return response.Success ? 
                this.Ok() : 
                this.Error(response.Error);
        }
    }
}