using System;

namespace QuizDesigner.Common.Outbox
{
    public interface IIntegrationEvent
    {
        Guid Id { get; }
    }
}