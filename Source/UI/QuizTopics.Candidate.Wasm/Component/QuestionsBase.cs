using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using QuizTopics.Candidate.Wasm.Services;
using QuizTopics.Candidate.Wasm.ViewModels;
using QuizTopics.Common.Results;

namespace QuizTopics.Candidate.Wasm.Component
{
    public class QuestionsBase : ComponentBase, IDisposable
    {
        private Guid currentQuestionId;

        private DotNetObjectReference<QuestionsBase> objRef;

        protected string SelectedAnswerId { get; set; } = string.Empty;

        [Parameter] public string ExamId { get; set; }

        [Inject] private IExamDataService ExamDataService { get; set; }

        [Inject] private INotificationService NotificationService { get; set; }

        [Inject] private IJSRuntime JsRuntime { get; set; }

        protected Collection<ExamAnswerViewModel> ExamAnswerViewModelCollection { get; private set; }
            = new BindingList<ExamAnswerViewModel>
            {
                new(), new(), new(), new()
            };

        protected string QuestionText { get; private set; }

        protected bool IsSelectAnswerButtonVisible => !string.IsNullOrEmpty(this.SelectedAnswerId);

        protected bool IsExamFinished { get; private set; }

        protected int SecondsLeft { get; private set; }

        protected async Task FailQuestionInternalAsync()
        {
            if (!string.IsNullOrEmpty(this.SelectedAnswerId))
            {
                await this.OnSelectAnswerAsync();
            }
            else
            {
                await this.MarkQuestionAsFailedAsync();
            }

            await this.ShowNextQuestionAsync();
            this.StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            await this.ShowNextQuestionAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                this.objRef = DotNetObjectReference.Create(this);
                await this.JsRuntime.InvokeVoidAsync("simpleCountdown.setNetObject", this.objRef);
            }
        }

        protected async Task OnSelectAnswerAsync()
        {
            var result = await this.ExamDataService.SelectExamAnswer(Guid.Parse(this.ExamId), this.currentQuestionId, Guid.Parse(this.SelectedAnswerId));
            if (result.Success)
            {
                await this.ShowNextQuestionAsync();
            }
            else
            {
                await this.ShowErrorNotification();
            }

            this.SelectedAnswerId = string.Empty;
        }

        private async Task ShowNextQuestionAsync()
        {
            var result = await this.ExamDataService.GetExamQuestionAsync(Guid.Parse(this.ExamId));
            if (result.Failure)
            {
                await this.ShowErrorNotification();
                return;
            }

            if (result.Value.Id == Guid.Empty)
            {
                this.IsExamFinished = true;
                await this.JsRuntime.InvokeVoidAsync("simpleCountdown.stop");
                return;
            }

            await this.SetQuestionAsync(result);

            this.ExamAnswerViewModelCollection = new BindingList<ExamAnswerViewModel>(
                result.Value.ExamAnswerViewModelsCollection.ToList());
        }

        private async Task SetQuestionAsync(Result<ExamQuestionViewModel> result)
        {
            this.QuestionText = result.Value.Text;
            this.currentQuestionId = result.Value.Id;

            await this.JsRuntime.InvokeVoidAsync("simpleCountdown.initialize", 5);
        }

        private async Task MarkQuestionAsFailedAsync()
        {
            var result = await this.ExamDataService.MarkQuestionAsFailed(Guid.Parse(this.ExamId), this.currentQuestionId);
            if (result.Failure)
            {
                await this.ShowErrorNotification();
            }
            else
            {
                await this.NotificationService.Warning("Question mark as failed", "No answer selected");
            }
        }

        private async Task ShowErrorNotification()
        {
            await this.NotificationService.Error("Something went wrong, contact with admins", "Oh no!!!");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.objRef?.Dispose();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}