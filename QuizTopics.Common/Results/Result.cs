using QuizTopics.Common.Errors;

namespace QuizTopics.Common.Results
{
    public class Result
    {
        protected Result(Error error)
        {
            this.Success = false;
            this.Error = error;
        }

        protected Result()
        {
            this.Success = true;
        }

        public Error? Error { get; set; }

        public bool Success { get; }

        public bool Failure => !this.Success;

        public static Result Ok() => new();

        public static Result Fail(Error error)
        {
            return new(error);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new(value);
        }

        public static Result<T> Fail<T>(Error error)
        {
            return new(error);
        }
    }
}