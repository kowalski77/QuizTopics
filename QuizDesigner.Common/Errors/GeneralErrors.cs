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
    }
}