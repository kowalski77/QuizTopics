namespace QuizDesigner.Common.ResultModels
{
    public class ResultModel : IResultModel
    {
        protected ResultModel(ResultOperation resultOperation, bool success)
        {
            this.ResultOperation = resultOperation;
            this.Success = success;
        }

        public ResultOperation ResultOperation { get; }

        public bool Success { get; }

        public static IResultModel Ok()
        {
            return new ResultModel(ResultOperation.Ok, true);
        }

        public static IResultModel<T> Ok<T>(T value)
        {
            return new ResultModel<T>(value, true, ResultOperation.Ok);
        }

        public static IResultModel Fail(ResultOperation resultOperation)
        {
            return new ResultModel(resultOperation, false);
        }

        public static IResultModel<T> Fail<T>(ResultOperation resultOperation)
        {
            return new ResultModel<T>(default!, false, resultOperation);
        }
    }
}