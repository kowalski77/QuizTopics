using System;

namespace QuizTopics.Envelopes
{
    public class Envelope
    {
        public Envelope() { }

        private Envelope(object? result, ErrorResult? error, string? invalidField)
        {
            this.Result = result;
            this.ErrorCode = error?.Code;
            this.ErrorMessage = error?.Message;
            this.InvalidField = invalidField;
            this.TimeGenerated = DateTime.UtcNow;
        }

        public object? Result { get; set; }

        public string? ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public string? InvalidField { get; set; }

        public DateTime TimeGenerated { get; set; }

        public static Envelope Ok(object? result)
        {
            return new Envelope(result, null, null);
        }

        public static Envelope Error(ErrorResult? error, string? invalidField)
        {
            return new Envelope(null, error, invalidField);
        }
    }
}