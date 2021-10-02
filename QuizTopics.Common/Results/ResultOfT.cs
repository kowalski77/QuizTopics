using System;
using QuizTopics.Common.Errors;

namespace QuizTopics.Common.Results
{
    public sealed class Result<T> : Result
    {
        private readonly T value;

        public Result(T value)
        {
            this.value = value;
        }

        public Result(Error error) : base(error)
        {
            this.value = default!;
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