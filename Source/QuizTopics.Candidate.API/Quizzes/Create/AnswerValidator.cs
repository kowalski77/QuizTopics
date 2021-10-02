using FluentValidation;
using QuizTopics.Shared;

namespace QuizTopics.Candidate.API.Quizzes.Create
{
    public class AnswerValidator : AbstractValidator<Answer>
    {
        public AnswerValidator()
        {
            this.RuleFor(x => x.Text).NotEmpty().MaximumLength(500);
        }
    }
}