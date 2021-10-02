using MediatR;

namespace QuizTopics.Common.Mediator
{
    public interface IDomainEventHandler<in T> : INotificationHandler<T>
        where T : IDomainEvent
    {
    }
}