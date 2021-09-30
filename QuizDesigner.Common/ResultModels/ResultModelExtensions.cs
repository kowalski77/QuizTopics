using System;
using System.Threading.Tasks;

namespace QuizDesigner.Common.ResultModels
{
    public static class ResultModelExtensions
    {
        public static async Task<IResultModel<T>> OnSuccess<T>(this IResultModel resultModel, Func<Task<IResultModel<T>>> func)
        {
            return resultModel.Success ?
                await func() :
                ResultModel.Fail<T>(resultModel.Error);
        }

        public static async Task<IResultModel> OnSuccess<T>(this Task<IResultModel<T>> resultModel, Action<T> action)
        {
            var awaitedResultModel = await resultModel;
            if (!awaitedResultModel.Success)
            {
                return ResultModel.Fail(awaitedResultModel.Error);
            }

            action(awaitedResultModel.Value);

            return ResultModel.Ok();
        }

        public static async Task<IResultModel> OnSuccess(this Task<IResultModel> resultModel, Func<Task> action)
        {
            var awaitedResultModel = await resultModel;
            if (!awaitedResultModel.Success)
            {
                return ResultModel.Fail(awaitedResultModel.Error);
            }

            await action();

            return ResultModel.Ok();
        }

        public static async Task<IResultModel<TR>> OnSuccess<T, TR>(this Task<IResultModel<T>> resultModel, Func<T, IResultModel<TR>> func)
        {
            var awaitedResultModel = await resultModel;

            return awaitedResultModel.Success ?
                func(awaitedResultModel.Value) :
                ResultModel.Fail<TR>(awaitedResultModel.Error);
        }

        public static async Task<IResultModel> OnSuccess<T>(this Task<IResultModel<T>> resultModel, Func<T, Task<IResultModel>> func)
        {
            var awaitedResultModel = await resultModel;

            return awaitedResultModel.Success ?
                await func(awaitedResultModel.Value) :
                ResultModel.Fail(awaitedResultModel.Error);
        }

        public static async Task<IResultModel<TR>> OnSuccess<T, TR>(this Task<IResultModel<T>> resultModel, Func<T, Task<IResultModel<TR>>> func)
        {
            var awaitedResultModel = await resultModel;

            return awaitedResultModel.Success ?
                await func(awaitedResultModel.Value) :
                ResultModel.Fail<TR>(awaitedResultModel.Error);
        }
    }
}