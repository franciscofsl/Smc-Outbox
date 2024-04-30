using SmcOutbox.Worker.Cron;

namespace SmcOutbox.Worker.Jobs;

public class HolaJupiterExample : BaseCronWorker<HolaJupiterExample>
{
    public HolaJupiterExample(CronOptions<HolaJupiterExample> options) : base(options)
    {
    }

    protected override Task HandleAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine($"HOLA JUPITER: {DateTime.Now.ToShortDateString()} {DateTime.Now.TimeOfDay.ToString()}");

        return Task.CompletedTask;
    }
}