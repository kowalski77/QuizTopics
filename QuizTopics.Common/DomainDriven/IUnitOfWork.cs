using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuizTopics.Common.DomainDriven
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}