using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Candidate.API.Support;
using QuizTopics.Candidate.Application.Exams.Commands.Finish;
using QuizTopics.Common.Envelopes;

namespace QuizTopics.Candidate.API.Exams.Finish
{
    [Route("api/v1/[controller]"), Authorize]
    public class ExamController : ApplicationController
    {
        public ExamController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("{examId:guid}/finishExam")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Envelope))]
        public async Task<IActionResult> FinishExam(Guid examId)
        {
            if (examId == Guid.Empty)
            {
                throw new ArgumentException("Exam id is an empty guid", nameof(examId));
            }

            var resultModel = await this.Mediator.Send(new FinishExamCommand(examId)).ConfigureAwait(false);

            return this.FromResultModel(resultModel);
        }
    }
}