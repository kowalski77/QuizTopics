using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizDesigner.Common.Database;

namespace QuizTopics.Candidate.Application.Behaviors
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IDbContext dbContext;

        public TransactionBehaviour(IDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return this.HandleInternal(request, cancellationToken, next);
        }

        private async Task<TResponse> HandleInternal(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var strategy = this.dbContext.DatabaseFacade.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
           {
               Guid transactionId;
               await using var transaction = await this.dbContext.BeginTransactionAsync().ConfigureAwait(false);
               response = await next().ConfigureAwait(false);
               await this.dbContext.CommitTransactionAsync(transaction).ConfigureAwait(false);

               transactionId = transaction.TransactionId;

                //await this.outboxService.PublishTransactionEventsAsync(transactionId);
            })
                .ConfigureAwait(false);

            return response ?? throw new InvalidOperationException("Response is null");
        }
    }
}