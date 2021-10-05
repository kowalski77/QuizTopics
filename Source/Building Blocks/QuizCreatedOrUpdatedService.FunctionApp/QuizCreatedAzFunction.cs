using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using QuizCreatedOrUpdatedService.FunctionApp.Services;
using QuizTopics.Models;

namespace QuizCreatedOrUpdatedService.FunctionApp
{
    public class QuizCreatedAzFunction
    {
        private readonly IQuizService quizService;

        public QuizCreatedAzFunction(IQuizService quizService)
        {
            this.quizService = quizService ?? throw new ArgumentNullException(nameof(quizService));
        }

        [Function(nameof(QuizCreatedAzFunction))]
        public async Task Run([QueueTrigger("quizcreated", Connection = "StorageConnectionString")] string quizCreatedItem,
            FunctionContext context)
        {
            var quizModel = JsonSerializer.Deserialize<CreateQuizModel>(quizCreatedItem);
            await this.quizService.CreateQuizAsync(quizModel).ConfigureAwait(false);

            var logger = context.GetLogger(nameof(QuizCreatedAzFunction));
            logger.LogInformation($"C# Queue trigger function processed, quiz: {quizModel}");
        }
    }
}