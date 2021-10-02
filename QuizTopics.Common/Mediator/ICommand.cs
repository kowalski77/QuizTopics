using MediatR;

namespace QuizTopics.Common.Mediator
{
    public interface ICommand<out TCommand> : IRequest<TCommand>
    {
    }
}