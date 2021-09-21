using System;
using System.Threading.Tasks;
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
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddTransient<QuizTopicsApiAuthorizationHandler>();

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
                builder.Configuration.Bind("UserOptions", options.UserOptions);
            });

            builder.Services.AddHttpClient<ITestDataService, TestDataService>(
                    client => client.BaseAddress = new Uri("https://localhost:5003/"))
                .AddHttpMessageHandler<QuizTopicsApiAuthorizationHandler>();

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}