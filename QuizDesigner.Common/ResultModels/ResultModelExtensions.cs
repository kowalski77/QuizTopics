using System;
using System.Threading.Tasks;

namespace QuizDesigner.Common.ResultModels
{
    public static class ResultModelExtensions
    {
        public static IResultModel<T> OnSuccess<T>(this IResultModel resultModel, Func<T> func)
        {
            return !resultModel.Success ? 
                ResultModel.Fail<T>(resultModel.Error) : 
                ResultModel.Ok(func());
        }

        public static async Task<IResultModel> OnSuccess<T>(this IResultModel<T> resultModel, Func<T, Task<IResultModel>> func)
        {
            if (!resultModel.Success)
            {
                return ResultModel.Fail(resultModel.Error);
            }

            return await func(resultModel.Value).ConfigureAwait(false);
        }
    }
}