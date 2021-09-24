using FluentValidation;
using QuizDesigner.Common.Results.Extensions;
using QuizDesigner.Shared;

namespace QuizTopics.Candidate.API.Exams.Create
{
    public class CreateExamModelValidator : AbstractValidator<CreateExamModel>
    {
        public CreateExamModelValidator()
        {
            this.RuleFor(x => x.UserEmail.EnsureValidEmailAddress());
        }
    }
}