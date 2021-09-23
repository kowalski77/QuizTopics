﻿using System;
using System.Threading.Tasks;
using QuizDesigner.Common.Optional;

namespace QuizDesigner.Common.DomainDriven
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

        public async Task<Maybe<T>> GetAsync(Guid id)
        {
            return await this.Context.FindAsync<T>(id).ConfigureAwait(false);
        }
    }
}