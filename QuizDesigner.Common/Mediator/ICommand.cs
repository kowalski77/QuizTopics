using MediatR;

namespace QuizDesigner.Common.Mediator
{
    public interface ICommand<out TCommand> : IRequest<TCommand>
    {
    }
}