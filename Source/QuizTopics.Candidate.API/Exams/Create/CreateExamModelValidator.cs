using FluentValidation;
using QuizDesigner.Shared;
using QuizTopics.Common.Api;

namespace QuizTopics.Candidate.API.Exams.Create
{
    public class CreateExamModelValidator : AbstractValidator<CreateExamModel>
    {
        public CreateExamModelValidator()
        {
            this.RuleFor(x => x.UserEmail)
                .EnsureNotEmpty()
                .EnsureEmailAddress();
        }
    }
}