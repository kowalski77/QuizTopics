using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Candidate.Application.Exams.Commands.Finish;
using QuizTopics.Common.Api;

namespace QuizTopics.Candidate.API.Exams.Finish
{
    [Route("api/v1/[controller]"), Authorize]
    public class ExamController : ApplicationController
    {
        public ExamController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("{examId:guid}/finishExam")]
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