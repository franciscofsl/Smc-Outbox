namespace SmcOutbox.Core.Common;

public abstract class Aggregate : WithDomainEvents
{
    public Guid Id { get; protected set; }
}