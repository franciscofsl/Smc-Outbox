namespace SmcOutbox.Core.Common;

public interface IEventDispatcher
{
    Task Dispatch(IEvent command, CancellationToken token = default);
}