using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QuizTopics.Models;

namespace QuizTopics.Candidate.Wasm.Services
{
    public class ExamDataService : IExamDataService
    {
        private readonly HttpClient httpClient;

        public ExamDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task CreateExam()
        {
            var exam = new CreateExamModel("email@email.com", Guid.NewGuid());
            var examJson = new StringContent(JsonSerializer.Serialize(exam), Encoding.UTF8, "application/json");

            var response = await this.httpClient.PostAsync("api/v1/exam", examJson).ConfigureAwait(true);
        }
    }
}