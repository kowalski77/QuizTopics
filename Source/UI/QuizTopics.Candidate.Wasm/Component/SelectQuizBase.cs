using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using QuizTopics.Candidate.Wasm.Services;
using QuizTopics.Candidate.Wasm.ViewModels;

namespace QuizTopics.Candidate.Wasm.Component
{
    public class SelectQuizBase : ComponentBase
    {
        [Inject] private IQuizDataService QuizDataService { get; set; }

        protected IEnumerable<QuizViewModel> QuizViewModelCollection { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await this.QuizDataService.GetAsync();
            this.QuizViewModelCollection = result.Value;
        }

        protected void OnSelectedValueChanged()
        {

        }
    }
}