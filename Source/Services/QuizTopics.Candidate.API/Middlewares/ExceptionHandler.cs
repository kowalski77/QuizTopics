using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using QuizTopics.Common.Envelopes;

namespace QuizTopics.Candidate.API.Middlewares
{
    public sealed class ExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment env;

        public ExceptionHandler(RequestDelegate next, IWebHostEnvironment env)
        {
            this.next = next;
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            try
            {
                await this.next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await this.HandleException(context, ex).ConfigureAwait(false);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            var errorMessage = this.env.IsProduction() ? "Internal server error" : "Exception: " + exception.Message;
            var error = GeneralErrors.InternalServerError(errorMessage);
            var envelope = Envelope.Error(error, string.Empty);
            var result = JsonSerializer.Serialize(envelope);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(result);
        }
    }
}