using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using QuizTopics.Candidate.Wasm.ViewModels;
using QuizTopics.Common.Envelopes;
using QuizTopics.Common.Results;
using QuizTopics.Models;

namespace QuizTopics.Candidate.Wasm.Services
{
    public class QuizDataService : IQuizDataService
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient httpClient;

        public QuizDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Result<IEnumerable<QuizViewModel>>> GetAsync()
        {
            var response = await this.httpClient.GetAsync("api/v1/Quiz").ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync();
            var envelope = JsonSerializer.Deserialize<Envelope<IEnumerable<QuizModel>>>(content, JsonSerializerOptions) ??
                           throw new InvalidOperationException($"Failed when tried to deserialize: {typeof(Envelope<IEnumerable<QuizModel>>)}");

            return response.IsSuccessStatusCode ? 
                Result.Ok(envelope.Result.Select(x => (QuizViewModel)x)) : 
                Result.Fail<IEnumerable<QuizViewModel>>(new ErrorResult(envelope.ErrorCode, envelope.ErrorMessage));
        }
    }
}