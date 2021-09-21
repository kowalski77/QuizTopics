using FluentValidation;
using QuizTopics.Candidate.Application.Quizzes.Create;

namespace QuizTopics.Candidate.API.Quizzes.Create
{
    public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
    {
        public CreateQuizCommandValidator()
        {
            this.RuleFor(x => x.Exam).NotEmpty();
            this.RuleFor(x => x.Name).NotEmpty();
        }
    }
}