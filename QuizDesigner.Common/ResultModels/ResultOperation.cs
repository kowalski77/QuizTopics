using System;
using System.Collections.Generic;
using System.Linq;

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
            IReadOnlyList<string> results) : this(code)
        {
            this.ValidationErrors = results ?? throw new ArgumentNullException(nameof(results));
        }

        private ResultOperation(
            ResultCode code,
            string result) : this(code)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            this.ValidationErrors = new[] { result };
        }

        public IReadOnlyList<string> ValidationErrors { get; } = new List<string>();

        public static ResultOperation Ok => new(ResultCode.Ok);

        public static ResultOperation Fail(ResultCode resultCode, string result) => new(resultCode, result);

        public static ResultOperation Fail(ResultCode resultCode, IReadOnlyList<string> resultCollection) => new(resultCode, resultCollection);

        public override string ToString()
        {
            return $"{this.code} - {this.code.GetDescription()} {Environment.NewLine}Errors: {string.Join(Environment.NewLine, this.ValidationErrors.Select(x => x.ToString()))}";
        }
    }
}