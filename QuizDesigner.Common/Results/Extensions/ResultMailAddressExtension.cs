using System;
using System.Net.Mail;

namespace QuizDesigner.Common.Results.Extensions
{
    public static class ResultMailAddressExtension
    {
        public static Result<string> EnsureValidEmailAddress(this string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _ = new MailAddress(value);
                }
                
                return Result.Ok(value);
            }
            catch (FormatException e)
            {
                return Result.Fail<string>(value, string.Format(ResultConstants.InvalidEmailAddress, value, e.Message));
            }
        }
    }
}