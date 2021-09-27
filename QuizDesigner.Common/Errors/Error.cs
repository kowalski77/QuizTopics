using System;

namespace QuizDesigner.Common.Errors
{
    public class Error
    {
        private const string Separator = "||";

        public Error(
            string code,
            string message)
        {
            this.Code = code;
            this.Message = message;
            this.TimeGenerated = DateTime.UtcNow;
        }

        public string Code { get; }

        public string Message { get; }

        public DateTime TimeGenerated { get; }

        public string Serialize()
        {
            return $"{this.Code}{Separator}{this.Message}";
        }
    }
}