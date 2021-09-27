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

        public static Error Deserialize(string serialized)
        {
            if (serialized == "A non-empty request body is required.")
            {
                return GeneralErrors.ValueIsRequired();
            }

            var data = serialized.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length < 2)
            {
                throw new Exception($"Invalid error serialization: '{serialized}'");
            }

            return new Error(data[0], data[1]);
        }
    }
}