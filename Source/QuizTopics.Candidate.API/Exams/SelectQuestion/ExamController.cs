using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizDesigner.Common.Api;
using QuizTopics.Candidate.Application.Exams;
using QuizTopics.Candidate.Application.Exams.Commands;
using QuizTopics.Candidate.Application.Exams.Commands.SelectQuestion;

namespace QuizTopics.Candidate.API.Exams.SelectQuestion
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

        [HttpGet("{examId:guid}/selectExamQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamQuestionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        public async Task<IActionResult> SelectExamQuestion(Guid examId)
        {
            if (examId == Guid.Empty)
            {
                throw new ArgumentException("Exam id is an empty guid", nameof(examId));
            }

            var resultModel = await this.mediator.Send(new SelectExamQuestionCommand(examId)).ConfigureAwait(false);

            return this.FromResultModel(resultModel);
        }
    }
}