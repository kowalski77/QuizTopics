using System.Threading.Tasks;
using QuizTopics.Shared;

namespace QuizCreatedOrUpdatedService.FunctionApp.Services
{
    public interface IQuizService
    {
        Task CreateQuizAsync(CreateQuizModel? quizModel);
    }
}