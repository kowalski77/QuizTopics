using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuizTopics.Candidate.Application.Behaviors;
using QuizTopics.Candidate.Application.Messaging;
using QuizTopics.Candidate.Application.Outbox;
using QuizTopics.Candidate.Application.Quizzes.Commands.Create;
using QuizTopics.Common.Database;
using QuizTopics.Common.Outbox;

namespace QuizTopics.Candidate.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateQuizCommand).Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));

            services.AddScoped<IMessagePublisher, TempMessagePublisher>();
            
            services.AddScoped<IOutboxService>(sp => new OutboxService(
                sp.GetRequiredService<IDbContext>(),
                sp.GetRequiredService<IMessagePublisher>(),
                dc => new OutboxRepository(dc),
                sp.GetRequiredService<ILogger<OutboxService>>()));

            return services;
        }
    }
}