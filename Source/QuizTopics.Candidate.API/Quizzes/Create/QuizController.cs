using System;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using QuizDesigner.Events;

namespace QuizTopics.Candidate.API.Quizzes.Create
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class QuizController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateQuiz([FromBody] QuizCreated quizCreated)
        {
            if (quizCreated == null)
            {
                throw new ArgumentNullException(nameof(quizCreated));
            }

            return this.Ok();
        }
    }
}