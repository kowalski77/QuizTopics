using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Common.Envelopes;

namespace QuizTopics.Candidate.API.Support
{
    public sealed class EnvelopeResult : IActionResult
    {
        private readonly Envelope envelope;
        private readonly int statusCode;

        public EnvelopeResult(Envelope envelope, HttpStatusCode statusCode)
        {
            this.statusCode = (int)statusCode;
            this.envelope = envelope;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this.envelope)
            {
                StatusCode = this.statusCode
            };

            return objectResult.ExecuteResultAsync(context);
        }
    }
}