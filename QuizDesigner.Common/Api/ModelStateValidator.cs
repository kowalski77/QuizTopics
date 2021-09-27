using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QuizDesigner.Common.Errors;

namespace QuizDesigner.Common.Api
{
    public sealed class ModelStateValidator
    {
        public static IActionResult ValidateModelState(ActionContext context)
        {
            (string fieldName, ModelStateEntry entry) = context.ModelState.First(x => x.Value.Errors.Count > 0);
            string errorSerialized = entry.Errors.First().ErrorMessage;

            var error = Error.Deserialize(errorSerialized);
            var envelope = Envelope.Error(error, fieldName);
            var envelopeResult = new EnvelopeResult(envelope, HttpStatusCode.BadRequest);

            return envelopeResult;
        }
    }
}