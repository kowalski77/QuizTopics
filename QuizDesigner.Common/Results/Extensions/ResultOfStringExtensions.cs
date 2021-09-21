namespace QuizDesigner.Common.Results.Extensions
{
    public static class ResultOfStringExtensions
    {
        public static Result<string> EnsureNotNullOrEmpty(this string text, string field) =>
            string.IsNullOrEmpty(text) ?
                Result.Fail<string>(field, ResultConstants.NotNullOrEmpty) :
                Result.Ok(text);

        public static Result<string> EnsureMaxLength(this string text, int length, string field) =>
            text.Length > length ?
                Result.Fail<string>(field, string.Format(ResultConstants.TooLong, length)) :
                Result.Ok(text);

        public static Result<string> EnsureMaxLength(this Result<string> result, int length, string field)
        {
            if (result.Failure)
            {
                return result;
            }

            return result.Value.Length > length ?
                   Result.Fail<string>(field, string.Format(ResultConstants.TooLong, length)) :
                   Result.Ok(result.Value);
        }
    }
}