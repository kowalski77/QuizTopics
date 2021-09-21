using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace QuizTopics.Candidate.API.TestController
{
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in this.User.Claims select new { c.Type, c.Value });
        }


    }
}