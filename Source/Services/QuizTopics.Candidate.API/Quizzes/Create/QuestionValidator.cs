using FluentValidation;
using QuizTopics.Models;

namespace QuizTopics.Candidate.API.Quizzes.Create
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            this.RuleFor(x => x.Text).NotEmpty().MaximumLength(500);
            this.RuleFor(x => x.Tag).NotEmpty().MaximumLength(50);
            this.RuleForEach(x => x.ExamAnswerCollection).SetValidator(new AnswerValidator());
        }
    }
}