using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using QuizTopics.Candidate.Wasm.Services;
using QuizTopics.Candidate.Wasm.ViewModels;

namespace QuizTopics.Candidate.Wasm.Component
{
    public class QuestionsBase : ComponentBase
    {
        [Parameter] public string ExamId { get; set; }

        [Inject] private IExamDataService ExamDataService { get; set; }

        [Inject] private INotificationService NotificationService { get; set; }

        protected Collection<ExamAnswerViewModel> ExamAnswerViewModelCollection { get; private set; }
            = new BindingList<ExamAnswerViewModel>
            {
                new(), new(), new(), new()
            };

        protected string QuestionText { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.ShowNextQuestionAsync();
        }

        private async Task ShowNextQuestionAsync()
        {
            var result = await this.ExamDataService.GetExamQuestionAsync(Guid.Parse(this.ExamId));
            if (result.Failure)
            {
                await this.NotificationService.Error("Something went wrong, contact with admins", "Oh no!!!");
                return;
            }

            this.QuestionText = result.Value.Text;
            this.ExamAnswerViewModelCollection = new BindingList<ExamAnswerViewModel>(
                result.Value.ExamAnswerViewModelsCollection.ToList());
        }
    }
}