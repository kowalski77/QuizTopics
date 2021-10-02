using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizDesigner.Common.Database;

namespace QuizTopics.Candidate.Application.Behaviors
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IDbContext dbContext;
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> logger;

        public TransactionBehaviour(IDbContext dbContext, ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

            this.logger.LogInformation($"{typeof(TransactionBehaviour<TRequest, TResponse>).Name} handling: {request.GetType().Name}");

            return this.HandleInternal(next, cancellationToken);
        }

        private async Task<TResponse> HandleInternal(
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var strategy = this.dbContext.DatabaseFacade.CreateExecutionStrategy();

            var response = await strategy.ExecuteAsync(async () => 
                    await this.ExecuteTransactionAsync(next, cancellationToken).ConfigureAwait(false))
                .ConfigureAwait(false);

            return response ?? throw new InvalidOperationException("Response is null");
        }

        private async Task<TResponse> ExecuteTransactionAsync(RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            await using var transaction = await this.dbContext.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
            var response = await next().ConfigureAwait(false);
            await this.dbContext.CommitTransactionAsync(transaction, cancellationToken).ConfigureAwait(false);

            //await this.outboxService.PublishTransactionEventsAsync(transaction.TransactionId);

            return response;
        }
    }
}