using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SmcOutbox.Application;

public static class Services
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}