using SmcOutbox.Core.Common;
using SmcOutbox.Infrastructure.BackgroundJobs;
using SmcOutbox.Worker.Cron;

namespace SmcOutbox.Worker.ServiceBus;

public class LocalOutboxMessagesJob : BaseCronWorker<LocalOutboxMessagesJob>
{
    private readonly IEventDispatcher _publisher;
    private readonly OutboxMessageStore _outboxMessageStore;

    public LocalOutboxMessagesJob(CronOptions<LocalOutboxMessagesJob> options, IEventDispatcher publisher,
        OutboxMessageStore outboxMessageStore) : base(options)
    {
        _publisher = publisher;
        _outboxMessageStore = outboxMessageStore;
    }

    protected override async Task HandleAsync(CancellationToken stoppingToken)
    {
        var messages = await _outboxMessageStore.GetNotProcessedMessagesAsync(stoppingToken);

        foreach (var outboxMessage in messages)
        {
            if (outboxMessage.ToDomainEvent() is not { } domainEvent)
            {
                continue;
            }

            await _publisher.Dispatch(domainEvent, stoppingToken);

            outboxMessage.ProcessedOn = DateTime.UtcNow;
        }

        await _outboxMessageStore.SaveAsync();
    }
}