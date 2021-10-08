using System;
using System.Threading.Tasks;
using QuizTopics.Candidate.Wasm.ViewModels;
using QuizTopics.Common.Results;

namespace QuizTopics.Candidate.Wasm.Services
{
    public interface IExamDataService
    {
        Task<Result> CheckExamAsync(string user, Guid quizId);

        Task<Result<ExamViewModel>> CreateExamAsync(string user, Guid quizId);
    }
}