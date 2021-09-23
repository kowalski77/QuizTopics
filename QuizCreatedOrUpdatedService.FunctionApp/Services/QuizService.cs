using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel.Client;
using QuizDesigner.Shared;

namespace QuizCreatedOrUpdatedService.FunctionApp.Services
{
    public class QuizService : IQuizService
    {
        private readonly HttpClient httpClient;

        public QuizService(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task CreateQuizAsync(QuizModel quizModel)
        {
            var discoveryResponse = await this.httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (discoveryResponse.IsError)
            {
                throw new InvalidOperationException(discoveryResponse.Error);
            }

            var tokenResponse = await this.httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = "azfunctionclient",
                ClientSecret = "secret",

                Scope = "candidateapi"
            });
            this.httpClient.SetBearerToken(tokenResponse.AccessToken);

            var quizJson = new StringContent(JsonSerializer.Serialize(quizModel), Encoding.UTF8, "application/json");
            var response = await this.httpClient.PostAsync("api/v1/Quiz", quizJson);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(response.ReasonPhrase);
            }
        }
    }
}