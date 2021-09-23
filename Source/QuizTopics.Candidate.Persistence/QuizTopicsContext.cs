using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizDesigner.Common.DomainDriven;
using QuizTopics.Candidate.Domain;

namespace QuizTopics.Candidate.Persistence
{
    public sealed class QuizTopicsContext : BaseContext
    {
        public QuizTopicsContext(DbContextOptions options, IMediator mediator)
            : base(options, mediator)
        {
        }

        public DbSet<Quiz>? Quizzes { get; set; }
    }
}