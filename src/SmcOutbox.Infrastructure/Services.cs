using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SmcOutbox.Infrastructure.BackgroundJobs;

namespace SmcOutbox.Infrastructure;

public static class Services
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddQuartz(configurator =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configurator
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger =>
                    trigger
                        .ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule
                                .WithIntervalInSeconds(10)
                                .RepeatForever()));
        });

        services.AddQuartzHostedService();
        services.AddTransient<OutboxMessageStore>();
        services.AddTransient<ProcessOutboxMessagesJob>();
    }
}