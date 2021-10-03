using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Common.Api;
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