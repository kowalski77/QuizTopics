using FluentValidation;
using QuizTopics.Common.Api;
using QuizTopics.Models;

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