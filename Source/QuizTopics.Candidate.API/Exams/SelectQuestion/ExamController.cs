using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Candidate.Application.Exams;
using QuizTopics.Candidate.Application.Exams.Queries.SelectQuestion;
using QuizTopics.Common.Api;

namespace QuizTopics.Candidate.API.Exams.SelectQuestion
{
    [Route("api/v1/[controller]")]
    public class ExamController : ApplicationController
    {
        public ExamController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{examId:guid}/selectExamQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamQuestionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Envelope))]
        public async Task<IActionResult> SelectExamQuestion(Guid examId)
        {
            if (examId == Guid.Empty)
            {
                throw new ArgumentException("Exam id is an empty guid", nameof(examId));
            }

            var resultModel = await this.Mediator.Send(new SelectExamQuestionCommand(examId)).ConfigureAwait(false);

            return this.FromResultModel(resultModel);
        }
    }
}