using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace QuizTopics.Common.Database
{
    public interface IDbContext
    {
        DatabaseFacade DatabaseFacade { get; }

        bool HasActiveTransaction { get; }

        IDbContextTransaction GetCurrentTransaction();

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default);
    }
}