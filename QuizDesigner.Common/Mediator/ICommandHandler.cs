using MediatR;
using QuizDesigner.Common.ResultModels;

namespace QuizDesigner.Common.Mediator
{
    public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest, IResultModel> 
        where TRequest : IRequest<IResultModel>
    {
    }
}