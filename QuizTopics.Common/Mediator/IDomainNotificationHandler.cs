using MediatR;

namespace QuizTopics.Common.Mediator
{
    public interface IDomainNotificationHandler<in T> : INotificationHandler<T>
        where T : IDomainNotification
    {
    }
}