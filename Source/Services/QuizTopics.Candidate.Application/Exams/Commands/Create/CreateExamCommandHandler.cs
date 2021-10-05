﻿using System;
using System.Threading;
using System.Threading.Tasks;
using QuizTopics.Candidate.Domain.ExamsAggregate;
using QuizTopics.Candidate.Domain.QuizzesAggregate;
using QuizTopics.Common.DomainDriven;
using QuizTopics.Common.Errors;
using QuizTopics.Common.Mediator;
using QuizTopics.Common.ResultModels;

namespace QuizTopics.Candidate.Application.Exams.Commands.Create
{
    public class CreateExamCommandHandler : ICommandHandler<CreateExamCommand, IResultModel<CreateExamDto>>
    {
        private readonly IExamService examService;
        private readonly IRepository<Exam> examRepository;
        private readonly IRepository<Quiz> quizRepository;

        public CreateExamCommandHandler(IExamService examService, IRepository<Exam> examRepository, IRepository<Quiz> quizRepository)
        {
            this.examService = examService ?? throw new ArgumentNullException(nameof(examService));
            this.examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
            this.quizRepository = quizRepository ?? throw new ArgumentNullException(nameof(quizRepository));
        }

        public async Task<IResultModel<CreateExamDto>> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var resultModel = await ResultModel.Init()
                .OnSuccess(async () => await this.GetQuizAsync(request.QuizId, cancellationToken).ConfigureAwait(false))
                .OnSuccess(async quiz => await this.CreateExamAsync(quiz, request.UserEmail, cancellationToken).ConfigureAwait(false))
                .ConfigureAwait(false);

            return resultModel;
        }

        private async Task<IResultModel<Quiz>> GetQuizAsync(Guid quizId, CancellationToken cancellationToken)
        {
            var maybeQuiz = await this.quizRepository.GetAsync(quizId, cancellationToken).ConfigureAwait(false);

            return maybeQuiz.TryGetValue(out var quiz) ? 
                ResultModel.Ok(quiz) :
                ResultModel.Fail<Quiz>(GeneralErrors.NotFound(quizId)) ;
        }

        private async Task<IResultModel<CreateExamDto>> CreateExamAsync(Quiz quiz, string userEmail, CancellationToken cancellationToken)
        {
            var result = await this.examService.CreateExamAsync(quiz, userEmail, cancellationToken).ConfigureAwait(false);
            if (!result.Success)
            {
                return ResultModel.Fail<CreateExamDto>(result.Error);
            }

            var exam = this.examRepository.Add(result.Value);
            await this.examRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken).ConfigureAwait(false);

            return ResultModel.Ok(exam.AsExamDto());
        }
    }
}