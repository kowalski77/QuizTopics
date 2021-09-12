using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace QuizCreatedOrUpdated.FunctionApp
{
    public static class QuizUpdatedFunction
    {
        [FunctionName(nameof(QuizUpdatedFunction))]
        public static void Run([QueueTrigger("quizupdated", Connection = "StorageConnectionAppSetting")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}