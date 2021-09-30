using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizDesigner.Common.Api;
using QuizTopics.Candidate.Application.Exams;
using QuizTopics.Candidate.Application.Exams.Queries.GetSummary;

namespace QuizTopics.Candidate.API.Exams.Summary
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

        [HttpGet("examId:guid")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SummaryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetExamsCollection(Guid examId)
        {
            var resultModel = await this.mediator.Send(new GetSummaryRequest(examId)).ConfigureAwait(false);

            return this.FromResultModel(resultModel);
        }
    }
}