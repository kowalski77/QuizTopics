using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizCreatedOrUpdatedService.FunctionApp.Services;

[assembly: CLSCompliant(false)]
namespace QuizCreatedOrUpdatedService.FunctionApp
{
    public static class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureHostConfiguration(config =>
                {
                    config.AddJsonFile("appsettings.Development.json", false);
                })
                .ConfigureServices((context, services) =>
                {
                    services.Configure<FunctionOptions>(context.Configuration.GetSection(nameof(FunctionOptions)));
                    services.AddHttpClient<IQuizService, QuizService>(client =>
                    {
                        client.BaseAddress = new Uri(context.Configuration.GetValue<string>("FunctionOptions:QuizEndpoint"));
                    });
                })
                .Build();

            host.Run();
        }
    }
}