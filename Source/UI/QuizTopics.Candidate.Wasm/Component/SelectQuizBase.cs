using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using QuizTopics.Candidate.Wasm.Services;
using QuizTopics.Candidate.Wasm.ViewModels;

namespace QuizTopics.Candidate.Wasm.Component
{
    public class SelectQuizBase : ComponentBase
    {
        private Guid selectedQuizViewModel = Guid.Empty;
        private bool isButtonEnabled;

        [Inject] private IQuizDataService QuizDataService { get; set; }

        protected IEnumerable<QuizViewModel> QuizViewModelCollection { get; private set; }

        protected string ButtonClass => this.isButtonEnabled ? "input-group-text" : "input-group-text disabled";

        protected override async Task OnInitializedAsync()
        {
            var result = await this.QuizDataService.GetAsync();
            this.QuizViewModelCollection = result.Value.ToList();
        }

        protected void OnSelectedValueChanged(Guid id)
        {
            this.selectedQuizViewModel = id;
            this.isButtonEnabled = this.selectedQuizViewModel != Guid.Empty;
        }

        protected Task StartAsync()
        {
            throw new NotImplementedException();
        }
    }
}