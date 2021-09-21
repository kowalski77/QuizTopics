namespace QuizDesigner.Common.Results.Extensions
{
    public static class ResultOfNumberExtensions
    {
        public static Result<double> EnsureDoubleGreaterThanZero(this double value, string field) => value <= 0
            ? Result.Fail<double>(field, ResultConstants.GreaterThanZero)
            : Result.Ok(value);

        public static Result<int> EnsureIntGreaterThanZero(this int value, string field) => value <= 0
            ? Result.Fail<int>(field, ResultConstants.GreaterThanZero)
            : Result.Ok(value);

        
        public static Result<decimal> EnsureDecimalGreaterThanZero(this decimal value, string field) => value <= 0
            ? Result.Fail<decimal>(field, ResultConstants.GreaterThanZero)
            : Result.Ok(value);
    }
}