using MediatR;
using QuizTopics.Common.ResultModels;
using System.Threading;
using System.Threading.Tasks;

namespace QuizTopics.Candidate.Application.Exams.Queries.CheckExam
{
    public sealed class CheckExamRequestHandler : IRequestHandler<CheckExamRequest, IResultModel<bool>>
    {

        public Task<IResultModel<bool>> Handle(CheckExamRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}