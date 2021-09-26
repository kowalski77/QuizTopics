﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizDesigner.Common.DomainDriven;
using QuizTopics.Candidate.Application.Quizzes.Queries;
using QuizTopics.Candidate.Domain.Exams;
using QuizTopics.Candidate.Domain.Quizzes;

namespace QuizTopics.Candidate.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFramework(connectionString);
            services.AddRepository<Exam, QuizTopicsContext>();
            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IRepository<Quiz>, QuizRepository>();
            services.AddScoped<IQuizProvider, QuizProvider>();

            return services;
        }

        private static void AddEntityFramework(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<QuizTopicsContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null)));
        }
    }
}