using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizTopics.Candidate.Domain.ExamsAggregate;
using QuizTopics.Candidate.Domain.QuizzesAggregate;
using QuizTopics.Common.DomainDriven;

namespace QuizTopics.Candidate.Persistence
{
    public sealed class QuizTopicsContext : BaseContext
    {
        public QuizTopicsContext(DbContextOptions<QuizTopicsContext> options, IMediator mediator)
            : base(options, mediator)
        {
        }

        public DbSet<Quiz>? Quizzes { get; set; }

        public DbSet<Exam>? Exams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizTopicsContext).Assembly);
        }
    }
}