namespace QuizDesigner.Common.Results
{
    public class Result
    {
        protected Result(
            bool success,
            string field,
            string error)
        {
            this.Success = success;
            this.Field = field;
            this.Error = error;
        }

        public string Field { get; }

        public string Error { get; }

        public bool Success { get; }

        public bool Failure => !this.Success;

        public static Result Create(bool success, string text) => new(success, string.Empty, text);

        public static Result<T> Create<T>(T value, bool success, string text)
        {
            return new(value, success, string.Empty, text);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new(value, true, typeof(T).Name, string.Empty);
        }

        public static Result Fail(string field, string error)
        {
            return new(false, field, error);
        }

        public static Result<T> Fail<T>(string field, string error)
        {
            return new(default!, false, field, error);
        }
    }
}