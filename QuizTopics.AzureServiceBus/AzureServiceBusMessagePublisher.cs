using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using QuizDesigner.Events;
using QuizTopics.Candidate.Application.Messaging;

namespace QuizTopics.AzureServiceBus
{
    public sealed class AzureServiceBusMessagePublisher : IMessagePublisher, IAsyncDisposable
    {
        private static readonly ConcurrentDictionary<Type, ServiceBusSender> ServiceBusSenders = new();

        private readonly ServiceBusClient serviceBusClient;

        public AzureServiceBusMessagePublisher(IOptions<AzureServiceBusOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            this.serviceBusClient = new ServiceBusClient(options.Value.StorageConnectionString);
        }

        public async Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
        {
            if (integrationEvent == null)
            {
                throw new ArgumentNullException(nameof(integrationEvent));
            }

            var integrationEventType = integrationEvent.GetType();
            var sender = ServiceBusSenders.GetOrAdd(integrationEventType, this.serviceBusClient.CreateSender(integrationEventType.Name));

            var serializedIntegrationEvent = JsonSerializer.Serialize(integrationEvent, integrationEventType);
            await sender.SendMessageAsync(new ServiceBusMessage(serializedIntegrationEvent), cancellationToken).ConfigureAwait(false);
        }

        public async ValueTask DisposeAsync()
        {
            await this.serviceBusClient.DisposeAsync().ConfigureAwait(false);
            foreach (var serviceBusSender in ServiceBusSenders)
            {
                await serviceBusSender.Value.DisposeAsync().ConfigureAwait(false);
            }
        }
    }
}