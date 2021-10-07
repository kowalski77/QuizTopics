using System;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizTopics.Candidate.Wasm.MessageHandlers;
using QuizTopics.Candidate.Wasm.Services;

namespace QuizTopics.Candidate.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            builder.RootComponents.Add<App>("#app");

            builder.Services.AddTransient<QuizTopicsCandidateAuthorizationMessageHandler>();
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
                builder.Configuration.Bind("UserOptions", options.UserOptions);
            });
            builder.Services.AddAuthorizationCore();

            builder.Services.AddHttpClient<IExamDataService, ExamDataService>(client =>
                    client.BaseAddress = new Uri("https://localhost:5003"))
                .AddHttpMessageHandler<QuizTopicsCandidateAuthorizationMessageHandler>();

            await builder.Build().RunAsync();
        }
    }
}