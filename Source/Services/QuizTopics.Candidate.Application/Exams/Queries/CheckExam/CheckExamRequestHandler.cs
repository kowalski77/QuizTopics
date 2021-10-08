using System;
using MediatR;
using QuizTopics.Common.ResultModels;
using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Candidate.Domain.ExamsAggregate;

namespace QuizTopics.Candidate.Application.Exams.Queries.CheckExam
{
    public sealed class CheckExamRequestHandler : IRequestHandler<CheckExamRequest, IResultModel>
    {
        private readonly IExamRepository examRepository;

        public CheckExamRequestHandler(IExamRepository examRepository)
        {
            this.examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        }

        public async Task<IResultModel> Handle(CheckExamRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var maybeQuiz = await this.examRepository.GetExamByCandidateAndQuiz(request.UserEmail, request.QuizId, cancellationToken)
                .ConfigureAwait(false);

            return maybeQuiz.TryGetValue(out var exam) ? 
                ResultModel.Fail(ExamErrors.UserAlreadyTokeExam(request.UserEmail, exam.QuizName)) : 
                ResultModel.Ok();
        }
    }
}