using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using QuizTopics.Candidate.Wasm.Services;
using QuizTopics.Candidate.Wasm.ViewModels;

namespace QuizTopics.Candidate.Wasm.Component
{
    public class SelectQuizBase : ComponentBase
    {
        private Guid selectedQuizViewModel = Guid.Empty;
        private bool isButtonEnabled;

        [CascadingParameter]
        public Task<AuthenticationState> AuthState { get; set; }

        [Inject] private IQuizDataService QuizDataService { get; set; }

        [Inject] private IExamDataService ExamDataService { get; set; }

        [Inject] private INotificationService NotificationService { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }

        protected IEnumerable<QuizViewModel> QuizViewModelCollection { get; private set; }

        protected string ButtonClass => this.isButtonEnabled ? "input-group-text" : "input-group-text disabled";

        protected bool StartEnabled { get; private set; }

        protected string ExamName=>
            this.QuizViewModelCollection.FirstOrDefault(x => x.Id == this.selectedQuizViewModel)?.Name;

        protected override async Task OnInitializedAsync()
        {
            var result = await this.QuizDataService.GetAsync();
            this.QuizViewModelCollection = result.Value.ToList();
        }

        protected void OnSelectedValueChanged(Guid id)
        {
            this.selectedQuizViewModel = id;
            this.isButtonEnabled = this.selectedQuizViewModel != Guid.Empty;
            this.StartEnabled = false;
        }

        protected async Task SelectAsync()
        {
            if (this.selectedQuizViewModel == Guid.Empty)
            {
                return;
            }

            var result = await this.ExamDataService.CheckExamAsync(await this.GetUserAsync(), this.selectedQuizViewModel);
            if (result.Failure)
            {
                await this.NotificationService.Error(result.Error?.Message, result.Error?.Code);
                this.StartEnabled = false;
            }
            else
            {
                this.StartEnabled = true;
            }
        }

        protected async Task OnStartAsync()
        {
            var result = await this.ExamDataService.CreateExamAsync(await this.GetUserAsync(), this.selectedQuizViewModel);
            if (result.Failure)
            {
                await this.NotificationService.Error(result.Error?.Message, result.Error?.Code);
                this.StartEnabled = false;
            }

            this.NavigationManager.NavigateTo($"/exam/{result.Value.Id}");
        }

        private async Task<string> GetUserAsync() => (await this.AuthState).User.Identity?.Name ??
                                         throw new InvalidOperationException("Could not retrieve the user");
    }
}