using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizDesigner.Shared;
using QuizTopics.Common.Api;

namespace QuizTopics.Candidate.API.Exams.SetFailedQuestion
{
    [Route("api/v1/[controller]")]
    public class ExamController : ApplicationController
    {
        public ExamController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("{examId:guid}/setFailedExamQuestion")]
        public async Task<IActionResult> SetFailedExamQuestion(Guid examId, [FromBody] SetFailedExamQuestionModel model)
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