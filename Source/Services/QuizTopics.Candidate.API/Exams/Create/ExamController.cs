using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Common.Api;
using QuizTopics.Envelopes;
using QuizTopics.Models;

namespace QuizTopics.Candidate.API.Exams.Create
{
    [Route("api/v1/[controller]")]
    public class ExamController : ApplicationController
    {
        public ExamController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        public async Task<IActionResult> CreateExam([FromBody] ExamModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var resultModel = await this.Mediator.Send(model.AsCommand()).ConfigureAwait(false);

            return FromResultModel(resultModel, rm => rm.AsModel());
        }
    }
}