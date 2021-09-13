using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace QuizCreatedOrUpdatedService.FunctionApp
{
    public static class QuizCreatedFunction
    {
        [Function(nameof(QuizCreatedFunction))]
        public static void Run([QueueTrigger("quizcreated", Connection = "StorageConnectionAppSetting")] string quizCreatedItem,
            FunctionContext context)
        {
            var quizCreated = JsonSerializer.Deserialize<QuizDesignerEvents.QuizCreated>(quizCreatedItem);

            var logger = context.GetLogger("Function1");
            logger.LogInformation($"C# Queue trigger function processed: {quizCreatedItem}");
        }
    }
}