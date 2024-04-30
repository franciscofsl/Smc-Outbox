namespace SmcOutbox.Worker.Cron;

public static class Extensions
{
    public static IServiceCollection AddCronJob<TService>(this IServiceCollection services,
        Action<CronOptions<TService>> options)
        where TService : BaseCronWorker<TService>
    {
        var cronOptions = new CronOptions<TService>();

        options.Invoke(cronOptions);

        services.AddSingleton(cronOptions);
        services.AddHostedService<TService>();

        return services;
    }
}