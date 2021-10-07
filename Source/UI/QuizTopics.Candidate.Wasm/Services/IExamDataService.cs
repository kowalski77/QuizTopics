using System;
using System.Threading.Tasks;
using QuizTopics.Candidate.Wasm.ViewModels;
using QuizTopics.Common.Results;

namespace QuizTopics.Candidate.Wasm.Services
{
    public interface IExamDataService
    {
        Task<Result<ExamViewModel>> CreateExam(string user, Guid quizId);
    }
}