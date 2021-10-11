using System;
using Microsoft.AspNetCore.Builder;

namespace QuizTopics.Candidate.API.Middlewares
{
    public static class ExceptionHandlerMiddleware
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseMiddleware<ExceptionHandler>();

            return app;
        }
    }
}