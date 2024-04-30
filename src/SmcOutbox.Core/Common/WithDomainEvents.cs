namespace SmcOutbox.Core.Common;

public class WithDomainEvents
{
    private readonly List<IEvent> _domainEvents = new();

    public IReadOnlyList<IEvent> Events => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(IEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}