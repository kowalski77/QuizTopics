using System;
using System.Threading.Tasks;

namespace QuizDesigner.Common.Optional
{
    public readonly struct Monad<T>
    {
        private readonly T value;
        private readonly bool hasValue;

        private Monad(T value)
        {
            this.value = value;
            this.hasValue = true;
        }

        public Monad<TResult> Bind<TResult, TException>(Func<T, Monad<TResult>> convert, Action<TException> action)
            where TException : Exception
        {
            try
            {
                return this.Bind(convert);
            }
            catch (TException e)
            {
                action(e);
                return new Monad<TResult>(default!);
            }
        }

        public Monad<TResult> Bind<TResult>(Func<T, Monad<TResult>> convert)
        {
            return !this.hasValue ?
                new Monad<TResult>() :
                convert(this.value);
        }

        public async Task<Monad<TResult>> BindAsync<TResult, TException>(Func<T, Task<Monad<TResult>>> convert, Action<TException> action)
            where TException : Exception
        {
            try
            {
                return await this.BindAsync(convert);
            }
            catch (TException e)
            {
                action(e);
                return new Monad<TResult>(default!);
            }
        }

        public TResult Unwrap<TResult>(Func<T, TResult> some, Func<TResult> none)
        {
            return this.hasValue ?
                some(this.value) :
                none();
        }

        public static implicit operator Monad<T>(T value)
        {
            return value == null! ?
                new Monad<T>() :
                new Monad<T>(value);
        }

        private async Task<Monad<TResult>> BindAsync<TResult>(Func<T, Task<Monad<TResult>>> convert)
        {
            return !this.hasValue ?
                new Monad<TResult>() :
                await convert(this.value);
        }
    }
}