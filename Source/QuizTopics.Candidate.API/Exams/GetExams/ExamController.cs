using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizDesigner.Common.Api;
using QuizTopics.Candidate.Application.Exams.Queries;
using QuizTopics.Candidate.Application.Exams.Queries.GetExams;
using ExamDto = QuizTopics.Candidate.Application.Exams.Commands.ExamDto;

namespace QuizTopics.Candidate.API.Exams.GetExams
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExamDto>))]
        public async Task<IActionResult> GetExamsCollection()
        {
            var examsCollection = await this.mediator.Send(new GetExamsRequest()).ConfigureAwait(false);

            return this.Ok(examsCollection);
        }
    }
}