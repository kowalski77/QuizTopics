using System;
using System.Text.Json;

namespace QuizDesigner.Common.Outbox
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

        public static T Deserialize<T>(OutboxMessage outboxMessage)
        {
            if (outboxMessage == null) throw new ArgumentNullException(nameof(outboxMessage));

            var result = JsonSerializer.Deserialize<T>(outboxMessage.Data);

            return result!;
        }
    }
}