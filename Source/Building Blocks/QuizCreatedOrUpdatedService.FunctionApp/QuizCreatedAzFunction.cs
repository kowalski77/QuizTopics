using System;
using System.Runtime.Serialization;
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
            if (quizCreated is null)
            {
                throw new SerializationException($"Could not deserialize the event {nameof(QuizCreated)}");
            }

            await this.quizService.CreateQuizAsync(quizCreated.AsModel()).ConfigureAwait(false);

            var logger = context.GetLogger(nameof(QuizCreatedAzFunction));
            logger.LogInformation($"C# Queue trigger function processed, quiz: {quizCreated}");
        }
    }
}