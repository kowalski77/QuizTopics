using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel.Client;
using QuizTopics.Shared;

namespace QuizCreatedOrUpdatedService.FunctionApp.Services
{
    public class QuizService : IQuizService
    {
        private readonly HttpClient httpClient;

        public QuizService(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task CreateQuizAsync(CreateQuizModel? quizModel)
        {
            if (quizModel == null)
            {
                throw new ArgumentNullException(nameof(quizModel));
            }

            var discoveryResponse = await this.GetDiscoveryDocumentResponseAsync();
            await this.SetTokenAsync(discoveryResponse);

            var quizJson = new StringContent(JsonSerializer.Serialize(quizModel), Encoding.UTF8, "application/json");

            var response = await this.httpClient.PostAsync("api/v1/Quiz", quizJson);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"{response.ReasonPhrase}: {content}");
            }
        }

        private async Task<DiscoveryDocumentResponse> GetDiscoveryDocumentResponseAsync()
        {
            var discoveryResponse = await this.httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (discoveryResponse.IsError)
            {
                throw new InvalidOperationException(discoveryResponse.Error);
            }

            return discoveryResponse;
        }

        private async Task SetTokenAsync(DiscoveryDocumentResponse discoveryResponse)
        {
            var tokenResponse = await this.httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = "azfunctionclient",
                ClientSecret = "secret",

                Scope = "candidateapi"
            });

            this.httpClient.SetBearerToken(tokenResponse.AccessToken);
        }
    }
}