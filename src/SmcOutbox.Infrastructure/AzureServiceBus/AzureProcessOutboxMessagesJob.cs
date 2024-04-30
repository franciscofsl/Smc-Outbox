using Azure.Messaging.ServiceBus;
using Quartz;
using SmcOutbox.Infrastructure.BackgroundJobs;
using SmcOutbox.Shared;

namespace SmcOutbox.Infrastructure.AzureServiceBus;

[DisallowConcurrentExecution]
public class AzureProcessOutboxMessagesJob(OutboxMessageStore outboxMessageStore) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await outboxMessageStore.GetNotProcessedMessagesAsync(context.CancellationToken);

        var client = new ServiceBusClient(Common.ConnectionString);
        var sender = client.CreateSender(Common.TopicName);
        
        foreach (var outboxMessage in messages)
        {
            var message = new ServiceBusMessage(outboxMessage.Content);

            await sender.SendMessageAsync(message);

            outboxMessage.ProcessedOn = DateTime.UtcNow;
        }

        await outboxMessageStore.SaveAsync();
    }
}