using System;
using System.Net;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizDesigner.Common.Errors;
using QuizDesigner.Common.ResultModels;

namespace QuizDesigner.Common.Api
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

        protected new IActionResult Ok(object? result)
        {
            return new EnvelopeResult(Envelope.Ok(result), HttpStatusCode.OK);
        }

        protected IActionResult NotFound(Error error, string? invalidField)
        {
            return new EnvelopeResult(Envelope.Error(error, invalidField), HttpStatusCode.NotFound);
        }

        protected IActionResult Error(Error? error, string? invalidField)
        {
            return new EnvelopeResult(Envelope.Error(error, invalidField), HttpStatusCode.BadRequest);
        }

        protected IActionResult FromResultModel<T>(IResultModel<T> result)
        {
            IActionResult actionResult = (result.Success, result.Error?.Code) switch
            {
                (true, _) => this.Ok(result.Value),
                (false, ErrorConstants.RecordNotFound) => this.NotFound(result.Error, null),
                _ => this.Error(result.Error, null)
            };

            return actionResult;
        }

        protected IActionResult FromResultModel(IResultModel result)
        {
            IActionResult actionResult = (result.Success, result.Error?.Code) switch
            {
                (true, _) => this.Ok(),
                (false, ErrorConstants.RecordNotFound) => this.NotFound(result.Error, null),
                _ => this.Error(result.Error, null)
            };

            return actionResult;
        }
    }
}