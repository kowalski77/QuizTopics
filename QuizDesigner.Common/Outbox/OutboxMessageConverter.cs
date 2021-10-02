using System;
using System.Reflection;
using Newtonsoft.Json;

namespace QuizDesigner.Common.Outbox
{
    public class OutboxMessageConverter : IOutboxMessageConverter
    {
        public T? Deserialize<T>(Type assemblyType, OutboxMessage outboxMessage)
            where T : class
        {
            var assembly = Assembly.GetAssembly(assemblyType) ?? throw new InvalidOperationException($"Could not find assembly with type {nameof(assemblyType)}");
            var type = assembly.GetType(outboxMessage.Type) ?? throw new InvalidOperationException($"Could not find type {outboxMessage.Type}");
            
            var result = JsonConvert.DeserializeObject(outboxMessage.Data, type) as T;

            return result;
        }
    }
}