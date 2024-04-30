using SmcOutbox.Worker.Cron;

namespace SmcOutbox.Worker.Jobs;

public class HolaTierraExample : BaseCronWorker<HolaTierraExample>
{
    public HolaTierraExample(CronOptions<HolaTierraExample> options) : base(options)
    {
    }

    protected override Task HandleAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine($"HOLA TIERRA: {DateTime.Now.ToShortDateString()} {DateTime.Now.TimeOfDay.ToString()}");

        return Task.CompletedTask;
    }
}