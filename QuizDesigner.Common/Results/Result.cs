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

        public static Result Ok() => new Result(true, string.Empty, string.Empty);

        public static Result Fail(string field, string error)
        {
            return new(false, field, error);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new(value, true, typeof(T).Name, string.Empty);
        }

        public static Result<T> Fail<T>(string field, string error)
        {
            return new(default!, false, field, error);
        }
    }
}