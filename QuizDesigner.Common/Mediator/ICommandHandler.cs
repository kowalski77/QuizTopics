using MediatR;

namespace QuizDesigner.Common.Mediator
{
    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
        where TRequest : ICommand<TResponse>
    {
    }
}