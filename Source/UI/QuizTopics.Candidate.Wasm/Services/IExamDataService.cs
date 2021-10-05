using System.Threading.Tasks;

namespace QuizTopics.Candidate.Wasm.Services
{
    public interface IExamDataService
    {
        Task CreateExam(string user);
    }
}