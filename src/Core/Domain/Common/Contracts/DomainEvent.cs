using NodeClutchGateway.Shared.Events;

namespace NodeClutchGateway.Domain.Common.Contracts;

public abstract class DomainEvent : IEvent
{
    public DateTime TriggeredOn { get; protected set; } = DateTime.UtcNow;
}