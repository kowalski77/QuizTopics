using System;
using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Common.Optional;

namespace QuizTopics.Common.DomainDriven
{
    public class BaseRepository<T> : IRepository<T>
        where T : class, IAggregateRoot
    {
        public BaseRepository(BaseContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected BaseContext Context { get; }

        public IUnitOfWork UnitOfWork => this.Context;

        public T Add(T item)
        {
            return this.Context.Add(item).Entity;
        }

        public virtual async Task<Maybe<T>> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await this.Context.FindAsync<T>(new object[] { id }, cancellationToken).ConfigureAwait(false);
        }
    }
}