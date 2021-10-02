﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizDesigner.Common.DomainDriven;
using QuizTopics.Candidate.Domain.ExamsAggregate;
using QuizTopics.Candidate.Domain.QuizzesAggregate;

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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizTopicsContext).Assembly);
        }
    }
}