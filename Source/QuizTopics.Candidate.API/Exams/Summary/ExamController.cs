using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Candidate.Application.Exams;
using QuizTopics.Candidate.Application.Exams.Queries.GetSummary;
using QuizTopics.Common.Api;

namespace QuizTopics.Candidate.API.Exams.Summary
{
    [Route("api/v1/[controller]")]
    public class ExamController : ApplicationController
    {
        public ExamController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{examId:guid}/summary")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SummaryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSummary(Guid examId)
        {
            var resultModel = await this.Mediator.Send(new GetSummaryRequest(examId)).ConfigureAwait(false);

            return this.FromResultModel(resultModel);
        }
    }
}