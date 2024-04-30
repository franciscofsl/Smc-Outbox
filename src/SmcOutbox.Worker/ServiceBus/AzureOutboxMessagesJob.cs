using Azure.Messaging.ServiceBus;
using SmcOutbox.Infrastructure.BackgroundJobs;
using SmcOutbox.Shared;
using SmcOutbox.Worker.Cron;

namespace SmcOutbox.Worker.ServiceBus;

public class AzureOutboxMessagesJob : BaseCronWorker<AzureOutboxMessagesJob>
{
    private readonly OutboxMessageStore _outboxMessageStore;

    public AzureOutboxMessagesJob(CronOptions<AzureOutboxMessagesJob> options,
        OutboxMessageStore outboxMessageStore) : base(options)
    {
        _outboxMessageStore = outboxMessageStore;
    }

    protected override async Task HandleAsync(CancellationToken stoppingToken)
    {
        var messages = await _outboxMessageStore.GetNotProcessedMessagesAsync(stoppingToken);

        var client = new ServiceBusClient(Common.ConnectionString);
        var sender = client.CreateSender(Common.TopicName);

        foreach (var outboxMessage in messages)
        {
            var message = new ServiceBusMessage(outboxMessage.Content);

            await sender.SendMessageAsync(message, stoppingToken);

            outboxMessage.ProcessedOn = DateTime.UtcNow;
        }

        await _outboxMessageStore.SaveAsync();
    }
}