using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace QuizCreatedOrUpdated.FunctionApp
{
    public static class QuizCreatedFunction
    {
        [FunctionName(nameof(QuizCreatedFunction))]
        public static void Run([QueueTrigger("quizcreated", Connection = "StorageConnectionAppSetting")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}