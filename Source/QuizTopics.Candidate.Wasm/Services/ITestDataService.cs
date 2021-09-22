using System.Threading.Tasks;
using QuizDesigner.Shared;

namespace QuizTopics.Candidate.Wasm.Services
{
    public interface ITestDataService
    {
        Task<Test> AddTestAsync();
    }
}