using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizTopics.Common.DomainDriven;

namespace QuizTopics.Common.Outbox
{
    public class OutboxContext : DbContext, IUnitOfWork
    {
        public OutboxContext(DbContextOptions<OutboxContext> options) 
            : base(options)
        {
        }

        public DbSet<OutboxMessage>? OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OutboxMessageEntityTypeConfiguration());
        }
        
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken) > 0;
            
            return result;
        }
    }
}