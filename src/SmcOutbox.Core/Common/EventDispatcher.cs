namespace SmcOutbox.Core.Common;

public class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    public async Task Dispatch(IEvent command, CancellationToken token = default)
    {
        var type = typeof(IEventHandler<>).MakeGenericType(command.GetType());
        dynamic handler = serviceProvider.GetService(type);
        await handler.Handle((dynamic)command, token);
    }
}