using FluentValidation;
using QuizTopics.Common.Api;
using QuizTopics.Models;

namespace QuizTopics.Candidate.API.Exams.Create
{
    public class CreateExamModelValidator : AbstractValidator<ExamModel>
    {
        public CreateExamModelValidator()
        {
            this.RuleFor(x => x.User)
                .EnsureNotEmpty()
                .EnsureEmailAddress();
        }
    }
}