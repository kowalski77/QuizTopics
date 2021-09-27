﻿using System;
using QuizDesigner.Common.Errors;

namespace QuizDesigner.Common.Api
{
    public class Envelope
    {
        private Envelope(object? result, Error? error, string? invalidField)
        {
            this.Result = result;
            this.ErrorCode = error?.Code;
            this.ErrorMessage = error?.Message;
            this.InvalidField = invalidField;
            this.TimeGenerated = DateTime.UtcNow;
        }

        public object? Result { get; }

        public string? ErrorCode { get; }

        public string? ErrorMessage { get; }

        public string? InvalidField { get; }

        public DateTime TimeGenerated { get; }

        public static Envelope Ok(object? result = null)
        {
            return new Envelope(result, null, null);
        }

        public static Envelope Error(Error? error, string? invalidField)
        {
            return new Envelope(null, error, invalidField);
        }
    }
}