using FluentValidation;
using QuizDesigner.Shared;

namespace QuizTopics.Candidate.API.Exams.Create
{
    public class CreateExamModelValidator : AbstractValidator<CreateExamModel>
    {
        public CreateExamModelValidator()
        {
            this.RuleFor(x => x.UserEmail).NotEmpty().EmailAddress();
        }
    }
}