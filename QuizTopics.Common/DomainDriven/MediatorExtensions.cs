using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace QuizTopics.Common.DomainDriven
{
    public static class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync(
            this IMediator mediator,
            DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async domainEvent =>
                {
                    await mediator.Publish(domainEvent).ConfigureAwait(false);
                });

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}