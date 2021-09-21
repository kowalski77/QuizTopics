using MediatR;
using QuizDesigner.Common.ResultModels;

namespace QuizDesigner.Common.Mediator
{
    public interface ICommand : IRequest<IResultModel>
    {
    }
}