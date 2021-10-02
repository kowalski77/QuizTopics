using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizDesigner.Common.Database;
using QuizDesigner.Common.DomainDriven;
using QuizDesigner.Common.Outbox;
using QuizTopics.Candidate.Application.Exams.Queries;
using QuizTopics.Candidate.Application.Quizzes.Queries;
using QuizTopics.Candidate.Domain.ExamsAggregate;
using QuizTopics.Candidate.Domain.QuizzesAggregate;

namespace QuizTopics.Candidate.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFramework(connectionString);

            services
                .AddRepository<Exam, QuizTopicsContext>()
                .AddScoped<IExamRepository, ExamRepository>()
                .AddScoped<IRepository<Quiz>, QuizRepository>()
                .AddScoped<IQuizProvider, QuizProvider>()
                .AddScoped<IExamProvider, ExamProvider>();

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