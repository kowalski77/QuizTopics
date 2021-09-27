using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Candidate.Application.Exams.Commands;
using QuizTopics.Candidate.Application.Exams.Commands.SelectQuestion;

namespace QuizTopics.Candidate.API.Exams.SelectQuestion
{
    [ApiController, Route("api/v1/[controller]"), Authorize]
    [Produces(MediaTypeNames.Application.Json), Consumes(MediaTypeNames.Application.Json)]
    public class ExamController : ControllerBase
    {
        private readonly IMediator mediator;

        public ExamController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{examId:guid}/selectExamQuestion")]
        public async Task<ActionResult<ExamQuestionDto>> SelectExamQuestion(Guid examId)
        {
            if (examId == Guid.Empty)
            {
                throw new ArgumentException("Exam id is an empty guid", nameof(examId));
            }

            var response = await this.mediator.Send(new SelectExamQuestionCommand(examId)).ConfigureAwait(false);
            if (!response.Success)
            {
                return this.BadRequest(response.ResultOperation.ToString());
            }

            return response.Value.TryGetValue(out var examQuestion) ? 
                this.Ok(examQuestion) : 
                this.Ok("No more questions available");
        }
    }
}