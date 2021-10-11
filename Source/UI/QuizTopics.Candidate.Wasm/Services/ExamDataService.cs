using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QuizTopics.Candidate.Wasm.ViewModels;
using QuizTopics.Common.Envelopes;
using QuizTopics.Common.Results;
using QuizTopics.Models;

namespace QuizTopics.Candidate.Wasm.Services
{
    public class ExamDataService : IExamDataService
    {
        private const string JsonMediaType = "application/json";
        private const string ExamApiRoute = "api/v1/Exam";

        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient httpClient;

        public ExamDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Result> CheckExamAsync(string user, Guid quizId)
        {
            var exam = new ExamModel(Guid.Empty, user, quizId);
            var examJson = new StringContent(JsonSerializer.Serialize(exam), Encoding.UTF8, JsonMediaType);

            var response = await this.httpClient.PostAsync($"{ExamApiRoute}/checkExam", examJson).ConfigureAwait(false);

            return response.IsSuccessStatusCode ?
                Result.Ok() :
                Result.Fail(await GetErrorResultAsync(response));
        }

        public async Task<Result<ExamViewModel>> CreateExamAsync(string user, Guid quizId)
        {
            var exam = new ExamModel(Guid.NewGuid(), user, quizId);
            var examJson = new StringContent(JsonSerializer.Serialize(exam), Encoding.UTF8, "application/json");

            var response = await this.httpClient.PostAsync(ExamApiRoute, examJson).ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync();
            var envelope = JsonSerializer.Deserialize<Envelope<ExamModel>>(content, JsonSerializerOptions) ??
                           throw new InvalidOperationException($"Failed when tried to deserialize: {typeof(Envelope<ExamModel>)}");

            return response.IsSuccessStatusCode ?
                Result.Ok((ExamViewModel)envelope.Result) :
                Result.Fail<ExamViewModel>(new ErrorResult(envelope.ErrorCode, envelope.ErrorMessage));
        }

        public async Task<Result<ExamQuestionViewModel>> GetExamQuestionAsync(Guid examId)
        {
            var response = await this.httpClient.GetAsync($"{ExamApiRoute}/{examId.ToString()}/selectExamQuestion");

            var content = await response.Content.ReadAsStringAsync();
            var envelope = JsonSerializer.Deserialize<Envelope<ExamQuestionModel>>(content, JsonSerializerOptions) ??
                                throw new InvalidOperationException($"Failed when tried to deserialize: {typeof(Envelope<ExamModel>)}");

            return response.IsSuccessStatusCode ?
                Result.Ok((ExamQuestionViewModel)envelope.Result) :
                Result.Fail<ExamQuestionViewModel>(new ErrorResult(envelope.ErrorCode, envelope.ErrorMessage));
        }

        public async Task<Result> SelectExamAnswer(Guid examId, Guid questionId, Guid answerId)
        {
            var examAnswer = new SelectExamAnswerModel(questionId, answerId);
            var examAnswerJson = new StringContent(JsonSerializer.Serialize(examAnswer), Encoding.UTF8, JsonMediaType);

            var response = await this.httpClient.PostAsync($"{ExamApiRoute}/{examId.ToString()}/selectExamAnswer", examAnswerJson).ConfigureAwait(false);

            return response.IsSuccessStatusCode ?
                Result.Ok() :
                Result.Fail(await GetErrorResultAsync(response));
        }

        public async Task<Result> MarkQuestionAsFailed(Guid examId, Guid questionId)
        {
            var model = new SetFailedExamQuestionModel(questionId);
            var modelJson = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, JsonMediaType);

            var response = await this.httpClient.PostAsync($"{ExamApiRoute}/{examId.ToString()}/setFailedExamQuestion", modelJson).ConfigureAwait(false);

            return response.IsSuccessStatusCode ?
                Result.Ok() :
                Result.Fail(await GetErrorResultAsync(response));
        }

        public async Task<Result> FinishExamAsync(Guid examId)
        {
            var response = await this.httpClient.PostAsync($"{ExamApiRoute}/{examId.ToString()}/finishExam", null!);

            return response.IsSuccessStatusCode ?
                Result.Ok() :
                Result.Fail(await GetErrorResultAsync(response));
        }

        private static async Task<ErrorResult> GetErrorResultAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            var envelope = JsonSerializer.Deserialize<Envelope>(content, JsonSerializerOptions) ??
                           throw new InvalidOperationException($"Failed when tried to deserialize: {typeof(Envelope)}");

            return new ErrorResult(envelope.ErrorCode, envelope.ErrorMessage);
        }
    }
}