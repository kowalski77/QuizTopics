using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QuizTopics.Candidate.Application.Behaviors;
using QuizTopics.Candidate.Application.Quizzes.Commands.Create;

namespace QuizTopics.Candidate.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateQuizCommand).Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));

            return services;
        }
    }
}