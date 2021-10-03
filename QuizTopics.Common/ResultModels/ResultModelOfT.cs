﻿using System;
using QuizTopics.Common.Errors;

namespace QuizTopics.Common.ResultModels
{
    public sealed class ResultModel<T> : ResultModel, IResultModel<T>
    {
        private readonly T value;

        public ResultModel(
            T value,
            ErrorResult? error) : base(error)
        {
            this.value = value;
        }

        public ResultModel(T value)
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