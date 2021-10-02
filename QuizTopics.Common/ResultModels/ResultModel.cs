using QuizTopics.Common.Errors;

namespace QuizTopics.Common.ResultModels
{
    public class ResultModel : IResultModel
    {
        protected ResultModel(Error? error)
        {
            this.Error = error;
            this.Success = false;
        }

        protected ResultModel()
        {
            this.Success = true;
        }

        public Error? Error { get; }

        public bool Success { get; }

        public static IResultModel Ok()
        {
            return new ResultModel();
        }

        public static IResultModel Init() => Ok();

        public static IResultModel<T> Ok<T>(T value)
        {
            return new ResultModel<T>(value);
        }

        public static IResultModel Fail(Error? error)
        {
            return new ResultModel(error);
        }

        public static IResultModel<T> Fail<T>(Error? error)
        {
            return new ResultModel<T>(default!, error);
        }

        public static IResultModel<T> Fail<T>(T value, Error? error)
        {
            return new ResultModel<T>(value, error);
        }
    }
}