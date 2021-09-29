using System;

namespace QuizDesigner.Common.Errors
{
    public static class GeneralErrors
    {
        public static Error NotFound(Guid id)
        {
            var forId = id == Guid.Empty ? "" : $" for Id '{id}'";
            return new Error(ErrorConstants.RecordNotFound, $"Record not found{forId}");
        }

        public static Error ValueIsRequired()
        {
            return new Error(ErrorConstants.ValueIsRequired, "Value is required");
        }

        public static Error NotValidEmailAddress()
        {
            return new Error(ErrorConstants.NotValidEmail, "Value is not a valid email.");
        }
    }
}