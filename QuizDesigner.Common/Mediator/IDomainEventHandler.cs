using MediatR;

namespace QuizDesigner.Common.Mediator
{
    public interface IDomainEventHandler<in T> : INotificationHandler<T>
        where T : IDomainEvent
    {
    }
}