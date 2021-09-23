using System.Threading.Tasks;
using QuizDesigner.Shared;

namespace QuizCreatedOrUpdatedService.FunctionApp.Services
{
    public interface IQuizService
    {
        Task<QuizModel?> CreateQuizAsync(QuizModel quizModel);
    }
}