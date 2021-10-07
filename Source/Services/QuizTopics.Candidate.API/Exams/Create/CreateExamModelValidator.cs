using FluentValidation;
using QuizTopics.Candidate.API.Support;
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