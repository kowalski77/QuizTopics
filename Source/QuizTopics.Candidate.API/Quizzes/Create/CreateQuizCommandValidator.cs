using FluentValidation;
using QuizDesigner.Shared;

namespace QuizTopics.Candidate.API.Quizzes.Create
{
    public class CreateQuizCommandValidator : AbstractValidator<CreateQuizModel>
    {
        public CreateQuizCommandValidator()
        {
            this.RuleFor(x => x.Exam).NotEmpty();
            this.RuleFor(x => x.Name).NotEmpty();
        }
    }
}