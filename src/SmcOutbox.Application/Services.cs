using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SmcOutbox.Application.Meetings.Events;
using SmcOutbox.Core.Common;
using SmcOutbox.Core.Meetings.Events;

namespace SmcOutbox.Application;

public static class Services
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IEventDispatcher, EventDispatcher>();
        services.AddTransient(typeof(IEventHandler<MeetingCreated>), typeof(MeetingCreatedEventHandler));

        // services.AddTransient(typeof(INotificationHandler<MeetingCreated>), typeof(MeetingCreatedCommandHandler));
    }
}