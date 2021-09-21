using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using QuizDesigner.Events;

namespace QuizCreatedOrUpdatedService.FunctionApp
{
    public class QuizCreatedAzFunction
    {
        [Function(nameof(QuizCreatedAzFunction))]
        public static void Run([QueueTrigger("quizcreated", Connection = "StorageConnectionString")] string quizCreatedItem,
            FunctionContext context)
        {
            var quizCreated = JsonSerializer.Deserialize<QuizCreated>(quizCreatedItem);

            var logger = context.GetLogger("Function1");
            logger.LogInformation($"C# Queue trigger function processed: {quizCreatedItem}");
        }
    }
}