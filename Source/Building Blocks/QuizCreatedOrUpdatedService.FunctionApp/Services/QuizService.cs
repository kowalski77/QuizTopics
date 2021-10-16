#pragma warning disable CA2234 // Pass system uri objects instead of strings
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using QuizTopics.Models;

namespace QuizCreatedOrUpdatedService.FunctionApp.Services
{
    public class QuizService : IQuizService
    {
        private const string JsonMediaType = "application/json";

        private readonly HttpClient httpClient;
        private readonly FunctionOptions options;

        public QuizService(HttpClient httpClient, IOptions<FunctionOptions> options)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task CreateQuizAsync(CreateQuizModel? quizModel)
        {
            if (quizModel == null)
            {
                throw new ArgumentNullException(nameof(quizModel));
            }

            var discoveryResponse = await this.GetDiscoveryDocumentResponseAsync().ConfigureAwait(false);
            await this.SetTokenAsync(discoveryResponse).ConfigureAwait(false);

            using var quizJson = new StringContent(JsonSerializer.Serialize(quizModel), Encoding.UTF8, JsonMediaType);

            var response = await this.httpClient.PostAsync(this.options.PostEndPoint, quizJson).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new InvalidOperationException($"{response.ReasonPhrase}: {content}");
            }
        }

        private async Task<DiscoveryDocumentResponse> GetDiscoveryDocumentResponseAsync()
        {
            var discoveryResponse = await this.httpClient.GetDiscoveryDocumentAsync(this.options.IdentityServerEndPoint).ConfigureAwait(false);
            if (discoveryResponse.IsError)
            {
                throw new InvalidOperationException(discoveryResponse.Error);
            }

            return discoveryResponse;
        }

        private async Task SetTokenAsync(DiscoveryDocumentResponse discoveryResponse)
        {
            using var request = new ClientCredentialsTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = this.options.TokenCredentials.ClientId,
                ClientSecret = this.options.TokenCredentials.ClientSecret,
                Scope = this.options.TokenCredentials.Scope
            };

            var tokenResponse = await this.httpClient.RequestClientCredentialsTokenAsync(request).ConfigureAwait(false);
            this.httpClient.SetBearerToken(tokenResponse.AccessToken);
        }
    }
}