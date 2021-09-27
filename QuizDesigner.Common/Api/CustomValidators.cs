using FluentValidation;
using QuizDesigner.Common.Errors;

namespace QuizDesigner.Common.Api
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, TProperty> EnsureNotEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.NotEmpty().WithMessage(GeneralErrors.ValueIsRequired().Serialize());
        }

        public static IRuleBuilderOptions<T, string> EnsureEmailAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.EmailAddress().WithMessage(GeneralErrors.NotValidEmailAddress().Serialize());
        }
    }
}