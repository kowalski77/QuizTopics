using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QuizTopics.Envelopes;
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

        public async Task CreateExam(string user)
        {
            var exam = new ExamModel(Guid.NewGuid(), user, Guid.NewGuid());

            var examJson = new StringContent(JsonSerializer.Serialize(exam), Encoding.UTF8, "application/json");

            var response = await this.httpClient.PostAsync("api/v1/exam", examJson).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var newlyExam = JsonSerializer.Deserialize<Envelope<ExamModel>>(content, JsonSerializerOptions);
            }

            var errorContent = await response.Content.ReadAsStringAsync();

            var error = JsonSerializer.Deserialize<Envelope>(errorContent, JsonSerializerOptions);
        }
    }
}