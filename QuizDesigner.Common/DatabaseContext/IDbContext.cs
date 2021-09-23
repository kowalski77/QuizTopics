using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace QuizDesigner.Common.DatabaseContext
{
    public interface IDbContext
    {
        DatabaseFacade DatabaseFacade { get; }

        bool HasActiveTransaction { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task CommitTransactionAsync(IDbContextTransaction transaction);
    }
}