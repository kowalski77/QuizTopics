using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizDesigner.Shared;

namespace QuizTopics.Candidate.API.TestController
{
    [Route("api/test")]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateTest([FromBody] Test test)
        {
            return this.Created("test", test);
        }
    }
}