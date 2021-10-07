using System.Diagnostics.CodeAnalysis;

namespace QuizTopics.Common.Envelopes
{
    public class Envelope<T> : Envelope
    {
        public Envelope([DisallowNull] T result)
        {
            this.Result = result;
        }

        public T Result { get; set; }
    }
}