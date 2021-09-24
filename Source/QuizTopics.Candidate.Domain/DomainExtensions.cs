using System;
using Microsoft.Extensions.DependencyInjection;
using QuizTopics.Candidate.Domain.Exams;

namespace QuizTopics.Candidate.Domain
{
    public static class DomainExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddScoped<IExamService, ExamService>();

            return services;
        }
    }
}