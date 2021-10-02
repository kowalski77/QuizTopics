using QuizTopics.Common.Errors;

namespace QuizTopics.Common.ResultModels
{
    public interface IResultModel<out T> : IResultModel
    {
        T Value { get; }
    }

    public interface IResultModel
    {
        bool Success { get; }

        Error? Error { get; }
    }
}