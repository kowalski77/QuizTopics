using MediatR;

namespace QuizTopics.Common.Mediator
{
    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
        where TRequest : ICommand<TResponse>
    {
    }
}