using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Candidate.API.Support;
using QuizTopics.Common.Envelopes;
using QuizTopics.Models;

namespace QuizTopics.Candidate.API.Exams.SelectAnswer
{
    [Route("api/v1/[controller]")]
    public class ExamController : ApplicationController
    {
        public ExamController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("{examId:guid}/selectExamAnswer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Envelope))]
        public async Task<IActionResult> SelectExamAnswer(Guid examId, [FromBody] SelectExamAnswerModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var resultModel = await this.Mediator.Send(model.AsCommand(examId)).ConfigureAwait(false);

            return this.FromResultModel(resultModel);
        }
    }
}