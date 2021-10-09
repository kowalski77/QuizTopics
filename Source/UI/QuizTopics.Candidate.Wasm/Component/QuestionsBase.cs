﻿using System;
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
        private Guid currentQuestionId;

        protected string SelectedAnswerId { get; set; } = string.Empty;

        [Parameter] public string ExamId { get; set; }

        [Inject] private IExamDataService ExamDataService { get; set; }

        [Inject] private INotificationService NotificationService { get; set; }

        protected Collection<ExamAnswerViewModel> ExamAnswerViewModelCollection { get; private set; }
            = new BindingList<ExamAnswerViewModel>
            {
                new(), new(), new(), new()
            };

        protected string QuestionText { get; private set; }

        protected bool IsSelectAnswerButtonVisible => !string.IsNullOrEmpty(this.SelectedAnswerId);

        protected bool IsExamFinished { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            await this.ShowNextQuestionAsync();
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
                return;
            }

            this.QuestionText = result.Value.Text;
            this.currentQuestionId = result.Value.Id;

            this.ExamAnswerViewModelCollection = new BindingList<ExamAnswerViewModel>(
                result.Value.ExamAnswerViewModelsCollection.ToList());
        }

        private async Task ShowErrorNotification()
        {
            await this.NotificationService.Error("Something went wrong, contact with admins", "Oh no!!!");
        }
    }
}