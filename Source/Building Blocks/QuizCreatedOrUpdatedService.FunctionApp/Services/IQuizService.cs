using System.Threading.Tasks;
using QuizTopics.Models;

namespace QuizCreatedOrUpdatedService.FunctionApp.Services
{
    public interface IQuizService
    {
        Task CreateQuizAsync(CreateQuizModel? quizModel);
    }
}