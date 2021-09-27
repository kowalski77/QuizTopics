using System;

namespace QuizDesigner.Common.Errors
{
    public static class GeneralErrors
    {
        public static Error NotFound(Guid id)
        {
            var forId = id == Guid.Empty ? "" : $" for Id '{id}'";
            return new Error("record.not.found", $"Record not found{forId}");
        }

        public static Error ValueIsRequired()
        {
            return new Error("value.is.required", "Value is required");
        }

        public static Error NotValidEmailAddress()
        {
            return new Error("email.is.not.valid", "Value is not a valid email.");
        }
    }
}