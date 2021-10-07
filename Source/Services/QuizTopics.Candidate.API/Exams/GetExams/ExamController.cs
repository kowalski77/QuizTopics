using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Candidate.API.Support;
using QuizTopics.Candidate.Application.Exams;
using QuizTopics.Candidate.Application.Exams.Queries.GetExams;

namespace QuizTopics.Candidate.API.Exams.GetExams
{
    [Route("api/v1/[controller]")]
    public class ExamController : ApplicationController
    {
        public ExamController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExamDto>))]
        public async Task<IActionResult> GetExamsCollection()
        {
            var examsCollection = await this.Mediator.Send(new GetExamsRequest()).ConfigureAwait(false);

            return Ok(examsCollection);
        }
    }
}