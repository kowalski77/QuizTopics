using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using QuizCreatedOrUpdatedService.FunctionApp.Services;
using QuizDesigner.Events;

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
            var quizCreated = JsonSerializer.Deserialize<QuizCreated>(quizCreatedItem);
            var response = await this.quizService.CreateQuizAsync(quizCreated.AsQuizModel());

            var logger = context.GetLogger(nameof(QuizCreatedAzFunction));
            if (response != null)
            {
                logger.LogInformation($"C# Queue trigger function processed, quiz: {response}");
            }
            
            logger.LogError("TODO...");
        }
    }
}