namespace SmcOutbox.Worker.Cron;

public abstract class BaseCronWorker<TServiceType> : BackgroundService
    where TServiceType : BaseCronWorker<TServiceType>
{
    protected BaseCronWorker(CronOptions<TServiceType> options)
    {
        Options = options;
    }

    protected CronOptions<TServiceType> Options { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timeSpan = TimeSpan.FromHours(Options.Hours) +
                       TimeSpan.FromMinutes(Options.Minutes) +
                       TimeSpan.FromSeconds(Options.Seconds);

        using var tickTimer = new PeriodicTimer(timeSpan);

        while (await tickTimer.WaitForNextTickAsync(stoppingToken))
        {
            await HandleAsync(stoppingToken);
        }
    }

    protected abstract Task HandleAsync(CancellationToken stoppingToken);
}