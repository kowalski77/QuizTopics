using System;
using System.Collections.Generic;
using System.Linq;
using QuizDesigner.Common.Results;

namespace QuizDesigner.Common.ResultModels
{
    public sealed class ResultOperation
    {
        private readonly ResultCode code;

        private ResultOperation(ResultCode code)
        {
            this.code = code;
        }

        private ResultOperation(
            ResultCode code,
            IReadOnlyList<Result> results) : this(code)
        {
            if (results == null)
            {
                throw new ArgumentNullException(nameof(results));
            }

            this.ValidationErrors = results.Select(x =>
                new ResultError(x.Field, x.Error))
                .ToList();
        }

        private ResultOperation(
            ResultCode code,
            Result result) : this(code)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            this.ValidationErrors = new[]
            {
                new ResultError(result.Field, result.Error)
            };
        }

        public IReadOnlyList<ResultError> ValidationErrors { get; } = new List<ResultError>();

        public static ResultOperation Ok => new(ResultCode.Ok);

        public static ResultOperation Fail(ResultCode resultCode, Result result) => new(resultCode, result);

        public static ResultOperation Fail(ResultCode resultCode, IReadOnlyList<Result> resultCollection) => new(resultCode, resultCollection);

        public override string ToString()
        {
            return $"{this.code} - {this.code.GetDescription()} {Environment.NewLine}Errors: {string.Join(Environment.NewLine, this.ValidationErrors.Select(x => x.ToString()))}";
        }
    }
}