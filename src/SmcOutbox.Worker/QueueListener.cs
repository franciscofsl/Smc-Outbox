using Azure.Messaging.ServiceBus;
using SmcOutbox.Shared;

namespace SmcOutbox.Worker;

public class QueueListener : BackgroundService
{
    private readonly ServiceBusProcessor _processor;

    public QueueListener()
    {
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
        await args.CompleteMessageAsync(args.Message);
        
        Console.WriteLine($"Consumer 1 received message: {message}");
    }

    private static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}