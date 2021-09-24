using System;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.Optional;

namespace QuizDesigner.Common.DomainDriven
{
    public interface IRepository<T> : IAggregateRoot
        where T : class, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        T Add(T item);

        Task<Maybe<T>> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}