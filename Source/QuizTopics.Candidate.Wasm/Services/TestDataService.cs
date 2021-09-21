using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QuizDesigner.Shared;

namespace QuizTopics.Candidate.Wasm.Services
{
    public class TestDataService : ITestDataService
    {
        private readonly HttpClient httpClient;

        public TestDataService(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Test> AddRandomValue(Test test)
        {
            var randomJson = new StringContent(JsonSerializer.Serialize(test), Encoding.UTF8, "application/json");

            var response = await this.httpClient.PostAsync("api/test", randomJson).ConfigureAwait(true);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Test>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }
    }
}