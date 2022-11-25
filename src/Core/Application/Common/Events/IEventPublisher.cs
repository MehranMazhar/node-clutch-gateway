using NodeClutchGateway.Shared.Events;

namespace NodeClutchGateway.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}