﻿using System;
using System.Threading.Tasks;

namespace QuizDesigner.Common.ResultModels
{
    public static class ResultModelExtensions
    {
        public static async Task<IResultModel<T>> OnSuccess<T>(this IResultModel resultModel, Func<Task<IResultModel<T>>> func)
        {
            if (!resultModel.Success)
            {
                return ResultModel.Fail<T>(resultModel.Error);
            }

            return await func();
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
                ResultModel.Ok(await func(awaitedResultModel.Value)) :
                ResultModel.Fail(awaitedResultModel.Error);
        }
    }
}