using System.Linq;
using QuizDesigner.Common.Results;

namespace QuizDesigner.Common.ResultModels.Extensions
{
    public static class ResultModelValidationExtensions
    {
        public static IResultModel Validate(params Result[] results)
        {
            return results.Any(x => x.Failure) ? 
                ResultModel.Fail(ResultOperation.Fail(ResultCode.BadRequest, results.Where(x => x.Failure).ToList())) : 
                ResultModel.Ok();
        }

        public static IResultModel NotFound(string field, string value)
        {
            return ResultModel.Fail(ResultOperation.Fail(ResultCode.NotFound, Result.Fail($"{field}", $"{value} not found")));
        }

        public static IResultModel<T> NotFound<T>(string field, string value)
        {
            return ResultModel.Fail<T>(ResultOperation.Fail(ResultCode.NotFound, Result.Fail($"{field}", $"{value} not found")));
        }
    }
}