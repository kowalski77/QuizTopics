using System;

namespace QuizDesigner.Common.ResultModels
{
    public sealed class ResultModel<T> : ResultModel, IResultModel<T>
    {
        private readonly T value;

        public ResultModel(
            T value,
            bool success,
            ResultOperation error) : base(error, success)
        {
            this.value = value;
        }

        public T Value
        {
            get
            {
                if (!this.Success)
                {
                    throw new InvalidOperationException("The result object has no value.");
                }

                return this.value;
            }
        }
    }
}