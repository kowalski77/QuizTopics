using System.Collections.Generic;
using System.Threading.Tasks;
using QuizTopics.Candidate.Wasm.ViewModels;
using QuizTopics.Common.Results;

namespace QuizTopics.Candidate.Wasm.Services
{
    public interface IQuizDataService
    {
        Task<Result<IEnumerable<QuizViewModel>>> GetAsync();
    }
}