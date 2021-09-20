using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuizTopics.Candidate.API.Quizzes.Add
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class QuizController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public ActionResult<string> Get()
        {
            return this.User.Identity?.Name;
        }
    }
}