using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuizDesigner.Common.Database;
using QuizDesigner.Common.Outbox;
using QuizTopics.Candidate.Application.Behaviors;
using QuizTopics.Candidate.Application.Messaging;
using QuizTopics.Candidate.Application.Outbox;
using QuizTopics.Candidate.Application.Quizzes.Commands.Create;

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