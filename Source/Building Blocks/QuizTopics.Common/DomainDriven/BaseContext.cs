using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using QuizTopics.Common.Database;

namespace QuizTopics.Common.DomainDriven
{
    public abstract class BaseContext : DbContext, IDbContext, IUnitOfWork
    {
        private readonly IMediator mediator;
        private IDbContextTransaction? currentTransaction;

        protected BaseContext(DbContextOptions options, IMediator mediator) 
            : base(options)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public DatabaseFacade DatabaseFacade => base.Database;

        public bool HasActiveTransaction => this.currentTransaction != null;

        public IDbContextTransaction GetCurrentTransaction()
        {
            if (this.currentTransaction == null)
            {
                throw new InvalidOperationException("Current transaction is null");
            }

            return this.currentTransaction;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (this.currentTransaction != null)
            {
                throw new InvalidOperationException("There is already a transaction in progress.");
            }

            this.currentTransaction = await this.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken)
                .ConfigureAwait(false);

            return this.currentTransaction;
        }

        public Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction != this.currentTransaction)
            {
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
            }

            return this.TryCommitTransactionAsync(transaction, cancellationToken);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await this.mediator.DispatchDomainEventsAsync(this).ConfigureAwait(false);

            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0;
        }

        private async Task TryCommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            try
            {
                await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                this.RollbackTransaction();
                throw;
            }
            finally
            {
                if (this.currentTransaction != null)
                {
                    this.currentTransaction.Dispose();
                    this.currentTransaction = null;
                }
            }
        }

        private void RollbackTransaction()
        {
            try
            {
                this.currentTransaction?.Rollback();
            }
            finally
            {
                if (this.currentTransaction != null)
                {
                    this.currentTransaction.Dispose();
                    this.currentTransaction = null;
                }
            }
        }
    }
}
