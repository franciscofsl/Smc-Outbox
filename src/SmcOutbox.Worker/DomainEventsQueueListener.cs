using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using SmcOutbox.Core.Common;
using SmcOutbox.Shared;

namespace SmcOutbox.Worker;

public class DomainEventsQueueListener : BackgroundService
{
    private readonly ServiceBusProcessor _processor;
    private readonly IEventDispatcher _eventDispatcher;

    public DomainEventsQueueListener(IEventDispatcher eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
        var client = new ServiceBusClient(Common.ConnectionString);
        _processor = client.CreateProcessor(Common.QueueName);
        _processor.ProcessMessageAsync += MessageHandler;
        _processor.ProcessErrorAsync += ErrorHandler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _processor.StartProcessingAsync(stoppingToken);
    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var message = args.Message.Body.ToString();

        var domainEvent = GetDomainEvent(message);

        await _eventDispatcher.Dispatch(domainEvent!);

        await args.CompleteMessageAsync(args.Message);
    }

    private static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }

    private static IEvent? GetDomainEvent(string message)
    {
        var domainEvent = JsonConvert
            .DeserializeObject<IEvent>(
                message,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

        if (domainEvent is null)
        {
            throw new Exception($"{nameof(domainEvent)} is null");
        }

        return domainEvent;
    }
}