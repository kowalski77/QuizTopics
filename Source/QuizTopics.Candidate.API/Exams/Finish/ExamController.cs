using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizDesigner.Common.Api;
using QuizTopics.Candidate.Application.Exams.Commands.Finish;

namespace QuizTopics.Candidate.API.Exams.Finish
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

        [HttpPost("{examId:guid}/finishExam")]
        public async Task<IActionResult> FinishExam(Guid examId)
        {
            if (examId == Guid.Empty)
            {
                throw new ArgumentException("Exam id is an empty guid", nameof(examId));
            }

            var resultModel = await this.mediator.Send(new FinishExamCommand(examId)).ConfigureAwait(false);

            return this.FromResultModel(resultModel);
        }
    }
}