using System;
using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Common.Monad;

namespace QuizTopics.Common.DomainDriven
{
    public interface IRepository<T> : IAggregateRoot
        where T : class, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        T Add(T item);

        Task<Maybe<T>> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}