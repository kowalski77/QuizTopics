using System;
using System.Threading;
using System.Threading.Tasks;

namespace QuizDesigner.Common.DomainDriven
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}