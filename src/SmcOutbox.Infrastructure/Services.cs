using Microsoft.Extensions.DependencyInjection;
using SmcOutbox.Infrastructure.BackgroundJobs;

namespace SmcOutbox.Infrastructure;

public static class Services
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<OutboxMessageStore>();
    }
}