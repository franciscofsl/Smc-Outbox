namespace SmcOutbox.Worker.Cron;

public class CronOptions<TService> where TService : BaseCronWorker<TService>
{
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }
}