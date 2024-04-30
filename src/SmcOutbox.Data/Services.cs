using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmcOutbox.Core.Common;
using SmcOutbox.Core.Meetings;
using SmcOutbox.Data.Outbox;
using SmcOutbox.Data.Repositories;

namespace SmcOutbox.Data;

public static class Services
{
    public static void AddData(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>((sp, options) =>
                options
                    .UseSqlServer(
                        "Server=localhost,1433;Database=SmcOutbox;User=sa;Password=Semicrol_10;MultipleActiveResultSets=true;TrustServerCertificate=True;")
                    .AddInterceptors(sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>()),
            ServiceLifetime.Transient, ServiceLifetime.Transient);

        services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddTransient<ConvertDomainEventsToOutboxMessagesInterceptor>();
    }
}