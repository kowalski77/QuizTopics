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
            var examJson = new StringContent(JsonSerializer.Serialize(exam), Encoding.UTF8, "application/json");

            var response = await this.httpClient.PostAsync("api/v1/Exam/checkExam", examJson).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return Result.Ok();
            }

            var res = await response.Content.ReadAsStringAsync();

            var error = JsonSerializer.Deserialize<Envelope>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions) ??
                        throw new InvalidOperationException($"Failed when tried to deserialize: {typeof(Envelope)}");

            return Result.Fail(new ErrorResult(error.ErrorCode!, error.ErrorMessage!));
        }

        public async Task<Result<ExamViewModel>> CreateExamAsync(string user, Guid quizId)
        {
            var exam = new ExamModel(Guid.NewGuid(), user, quizId);
            var examJson = new StringContent(JsonSerializer.Serialize(exam), Encoding.UTF8, "application/json");

            var response = await this.httpClient.PostAsync("api/v1/exam", examJson).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var newlyExam = JsonSerializer.Deserialize<Envelope<ExamModel>>(content, JsonSerializerOptions) ??
                                throw new InvalidOperationException($"Failed when tried to deserialize: {typeof(Envelope<ExamModel>)}");

                return Result.Ok((ExamViewModel)newlyExam.Result);
            }

            var error = JsonSerializer.Deserialize<Envelope>(await response.Content.ReadAsStringAsync(), JsonSerializerOptions) ??
                        throw new InvalidOperationException($"Failed when tried to deserialize: {typeof(Envelope)}");

            return Result.Fail<ExamViewModel>(new ErrorResult(error.ErrorCode!, error.ErrorMessage!));
        }
    }
}