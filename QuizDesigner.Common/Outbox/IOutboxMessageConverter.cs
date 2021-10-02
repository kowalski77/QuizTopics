using System;

namespace QuizDesigner.Common.Outbox
{
    public interface IOutboxMessageConverter
    {
        T? Deserialize<T>(Type assemblyType, OutboxMessage outboxMessage)
            where T : class;
    }
}