using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizCreatedOrUpdatedService.FunctionApp.Services;

namespace QuizCreatedOrUpdatedService.FunctionApp
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services.AddHttpClient<IQuizService, QuizService>(client =>
                    {
                        client.BaseAddress = new Uri("http://localhost:5003");
                    });
                })
                .Build();

            host.Run();
        }
    }
}