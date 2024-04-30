using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SmcOutbox.Infrastructure.AzureServiceBus;
using SmcOutbox.Infrastructure.BackgroundJobs;

namespace SmcOutbox.Infrastructure;

public static class Services
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddQuartz(configurator =>
        {
            var jobKey = new JobKey(nameof(AzureProcessOutboxMessagesJob));

            configurator
                .AddJob<AzureProcessOutboxMessagesJob>(jobKey)
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
        services.AddTransient<AzureProcessOutboxMessagesJob>();
    }
}