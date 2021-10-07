using Microsoft.AspNetCore.Components;
using QuizTopics.Candidate.Wasm.Services;

namespace QuizTopics.Candidate.Wasm.Component
{
    public class SelectQuizBase : ComponentBase
    {
        [Inject] private IExamDataService ExamDataService { get; set; }


    }
}