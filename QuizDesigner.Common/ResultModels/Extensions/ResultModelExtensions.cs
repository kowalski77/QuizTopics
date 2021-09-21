using System;
using System.Threading.Tasks;

namespace QuizDesigner.Common.ResultModels.Extensions
{
    public static class ResultModelExtensions
    {
        public static async Task<IResultModel> OnSuccess(this IResultModel resultModel, Func<Task> func)
        {
            if (!resultModel.Success)
            {
                return ResultModel.Fail(resultModel.ResultOperation);
            }

            await func();

            return ResultModel.Ok();
        }

        public static async Task<IResultModel> OnFail(this Task<IResultModel> resultModel, Action<IResultModel> action)
        {
            var awaitedResult = await resultModel;
            if (awaitedResult.Success)
            {
                return ResultModel.Ok();
            }

            action(awaitedResult);

            return ResultModel.Fail(awaitedResult.ResultOperation);
        }
    }
}