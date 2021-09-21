using System;

namespace QuizDesigner.Common.Results.Extensions
{
    public static class ResultOfTExtensions
    {
        public static Result<TK> Map<T, TK>(this Result<T> result, Func<T, TK> func)
        {
            return result.Failure ? Result.Fail<TK>(result.Field, result.Error) : Result.Ok(func(result.Value));
        }
    }
}