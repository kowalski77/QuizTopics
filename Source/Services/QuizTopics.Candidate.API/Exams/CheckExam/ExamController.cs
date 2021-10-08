using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Candidate.API.Support;
using QuizTopics.Candidate.Application.Exams.Queries.CheckExam;
using QuizTopics.Models;

namespace QuizTopics.Candidate.API.Exams.CheckExam
{
    [Route("api/v1/[controller]")]
    public class ExamController : ApplicationController
    {
        public ExamController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("checkExam")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> CheckExam([FromBody] ExamModel examModel)
        {
            if (examModel == null)
            {
                throw new ArgumentNullException(nameof(examModel));
            }

            var request = new CheckExamRequest(examModel.User, examModel.QuizId);
            var resultModel = await this.Mediator.Send(request).ConfigureAwait(false);

            return this.FromResultModel(resultModel);
        }
    }
}