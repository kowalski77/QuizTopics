using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizTopics.Candidate.Domain.ExamsAggregate;
using QuizTopics.Candidate.Domain.QuizzesAggregate;
using QuizTopics.Common.Database;
using QuizTopics.Common.Outbox;

[assembly: CLSCompliant(false)]
namespace QuizTopics.Candidate.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFramework(connectionString);

            services
                .AddScoped<IExamRepository, ExamRepository>()
                .AddScoped<IQuizRepository, QuizRepository>();

            return services;
        }

        private static void AddEntityFramework(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<QuizTopicsContext>(options =>
                options.UseSqlServer(connectionString,
                    sqlOptions => 
                        sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null)));

            services.AddScoped<IDbContext, QuizTopicsContext>();

            services.AddDbContext<OutboxContext>(options =>
                    options.UseSqlServer(connectionString,
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(QuizTopicsContext).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                        }));
        }
    }
}