using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QuizDesigner.Common.Optional;
using QuizDesigner.Common.ResultModels;
using QuizDesigner.Common.Results;
using QuizTopics.Candidate.Application.Exams.Queries;
using QuizTopics.Candidate.Domain.Exams;

namespace QuizTopics.Candidate.Application.Exams.Commands.SelectQuestion
{
    public class SelectExamQuestionCommandHandler : IRequestHandler<SelectExamQuestionCommand, IResultModel<Maybe<ExamQuestionDto>>>
    {
        private readonly IExamRepository examRepository;

        public SelectExamQuestionCommandHandler(IExamRepository examRepository)
        {
            this.examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        }

        public async Task<IResultModel<Maybe<ExamQuestionDto>>> Handle(SelectExamQuestionCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var maybeExam = await this.examRepository.GetAsync(request.ExamId, cancellationToken).ConfigureAwait(false);
            if (!maybeExam.TryGetValue(out var exam))
            {
                var resultOperation = ResultOperation.Fail(ResultCode.BadRequest, Result.Fail(nameof(request.ExamId), $" could not find exam with id: {request.ExamId}"));

                return ResultModel.Fail(Maybe<ExamQuestionDto>.None,  resultOperation);
            }

            var maybeQuestion = exam.GetFirstAvailableExamQuestion();
            if (!maybeQuestion.TryGetValue(out var question))
            {
                return ResultModel.Ok(Maybe<ExamQuestionDto>.None);
            }

            Maybe<ExamQuestionDto> examQuestionDto = question.AsExamQuestionDto();

            return ResultModel.Ok(examQuestionDto);
        }
    }
}