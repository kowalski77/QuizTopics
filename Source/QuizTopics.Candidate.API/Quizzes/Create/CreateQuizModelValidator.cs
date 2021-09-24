using FluentValidation;
using QuizDesigner.Shared;

namespace QuizTopics.Candidate.API.Quizzes.Create
{
    public class CreateQuizModelValidator : AbstractValidator<CreateQuizModel>
    {
        public CreateQuizModelValidator()
        {
            this.RuleFor(x => x.Exam).NotEmpty().MaximumLength(200);
            this.RuleFor(x => x.Name).NotEmpty().MaximumLength(2000);
            this.RuleForEach(x => x.ExamQuestionCollection).SetValidator(new QuestionValidator());
        }
    }
}