using System;

namespace QuizDesigner.Common.ResultModels
{
    public class ResultError 
    {
        public ResultError(
            string field,
            string message)
        {
            this.Field = field;
            this.Message = message;
            this.TimeGenerated = DateTime.UtcNow;
        }

        public string Field { get; }

        public string Message { get; }

        public DateTime TimeGenerated { get; }

        public override string ToString()
        {
            return $"Field: {this.Field} - Error: {this.Message}";
        }
    }
}