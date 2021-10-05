using System;
using System.Reflection;
using System.Text.Json;

namespace QuizTopics.Common.Outbox
{
    public static class OutboxSerializer
    {
        public static OutboxMessage Serialize<T>(T message)
            where T : class
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var type = message.GetType();
            var data = JsonSerializer.Serialize(message, type);

            return new OutboxMessage(Guid.NewGuid(), DateTime.UtcNow, type.FullName ?? type.Name, data);
        }

        public static T Deserialize<T>(OutboxMessage outboxMessage, Assembly assembly)
        {
            if (outboxMessage == null) throw new ArgumentNullException(nameof(outboxMessage));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var type = assembly.GetType(outboxMessage.Type) ?? 
                       throw new InvalidOperationException($"Could not find type {outboxMessage.Type}");

            var result = (T)JsonSerializer.Deserialize(outboxMessage.Data, type)!;

            return result;
        }
    }
}