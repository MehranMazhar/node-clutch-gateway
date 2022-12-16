namespace NodeClutchGateway.Domain.Blockchain;
public class ComplainArrived : AuditableEntity, IAggregateRoot
{
    public Guid TransactionId { get; set; }
    public virtual Transaction Transaction { get; set; } = default!;

    public Guid RideId { get; set; }
    public virtual Ride Ride { get; set; } = default!;

    public ComplainArrived(Guid rideId)
    {
        RideId = rideId;
    }
}
