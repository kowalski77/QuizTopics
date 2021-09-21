using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QuizTopics.Candidate.API.TestController
{
    [Route("api/test")]
    [Authorize]
    public class TestController : ControllerBase
    {
        
    }
}