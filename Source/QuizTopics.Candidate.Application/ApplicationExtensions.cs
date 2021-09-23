using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QuizTopics.Candidate.Application.Quizzes.Create;

namespace QuizTopics.Candidate.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateQuizCommand).Assembly);

            return services;
        }
    }
}