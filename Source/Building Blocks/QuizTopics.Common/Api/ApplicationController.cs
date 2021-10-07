using System;
using System.Net;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizTopics.Common.ResultModels;
using QuizTopics.Envelopes;

namespace QuizTopics.Common.Api
{
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ApplicationController : ControllerBase
    {
        public ApplicationController(IMediator mediator)
        {
            this.Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected IMediator Mediator { get; }

        protected new static IActionResult Ok(object? result)
        {
            return new EnvelopeResult(Envelope.Ok(result), HttpStatusCode.OK);
        }

        protected static IActionResult Error(ErrorResult? error, string? invalidField)
        {
            return new EnvelopeResult(Envelope.Error(error, invalidField), HttpStatusCode.BadRequest);
        }

        protected static IActionResult FromResultModel<T>(IResultModel<T> result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            IActionResult actionResult = (result.Success, result.ErrorResult?.Code) switch
            {
                (true, _) => Ok(result.Value),
                (false, ErrorConstants.RecordNotFound) => NotFound(result.ErrorResult, null),
                _ => Error(result.ErrorResult, null)
            };

            return actionResult;
        }

        protected static IActionResult FromResultModel<T, TR>(IResultModel<T> result, Func<T,TR> converter)
            where TR : class
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            IActionResult actionResult = (result.Success, result.ErrorResult?.Code) switch
            {
                (true, _) => Ok(converter(result.Value)),
                (false, ErrorConstants.RecordNotFound) => NotFound(result.ErrorResult, null),
                _ => Error(result.ErrorResult, null)
            };

            return actionResult;
        }

        protected IActionResult FromResultModel(IResultModel result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            IActionResult actionResult = (result.Success, result.ErrorResult?.Code) switch
            {
                (true, _) => this.Ok(),
                (false, ErrorConstants.RecordNotFound) => NotFound(result.ErrorResult, null),
                _ => Error(result.ErrorResult, null)
            };

            return actionResult;
        }

        private static IActionResult NotFound(ErrorResult error, string? invalidField)
        {
            return new EnvelopeResult(Envelope.Error(error, invalidField), HttpStatusCode.NotFound);
        }
    }
}