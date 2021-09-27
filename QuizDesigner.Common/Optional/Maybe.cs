using System;

namespace QuizDesigner.Common.Optional
{
    public readonly struct Maybe<T>
        where T : class
    {
        private readonly T value;
        private readonly bool hasValue;

        private Maybe(T value)
        {
            this.value = value;
            this.hasValue = true;
        }

        public static Maybe<T> None => new();

        public static implicit operator Maybe<T>(T value)
        {
            return value == null! ?
                new Maybe<T>() :
                new Maybe<T>(value);
        }

        public bool TryGetValue(out T tryValue)
        {
            if (this.hasValue)
            {
                tryValue = this.value;
                return true;
            }

            tryValue = default!;

            return false;
        }

        public T GetValue()
        {
            if (this.hasValue)
            {
                return this.value;
            }

            throw new InvalidOperationException($"Value of {typeof(T).FullName} is null");
        }

        public T ValueOr(T defaultValue)
        {
            return this.hasValue ?
                this.value :
                defaultValue;
        }

        public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> convert)
            where TResult : class
        {
            return !this.hasValue ?
                new Maybe<TResult>() :
                convert(this.value);
        }

        public TResult Unwrap<TResult>(Func<T, TResult> some, Func<TResult> none)
        {
            return this.hasValue ?
                some(this.value) :
                none();
        }
    }
}