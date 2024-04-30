namespace SmcOutbox.Core.Common;

public interface IEventHandler<in TRequest>
    where TRequest : IEvent
{
    Task Handle(TRequest command, CancellationToken token = default);
}