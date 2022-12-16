using NodeClutchGateway.Domain.Catalog;

namespace NodeClutchGateway.Domain.Blockchain;
public class Transaction : AuditableEntity, IAggregateRoot
{
    public string From { get; set; }
    public string To { get; set; }
    public double Amount { get; set; }
    public Guid BlockId { get; set; }
    public virtual Block Block { get; set; } = default!;
    public virtual RideRequest RideRequest { get; set; } = default!;
    public virtual RideOffer RideOffer { get; set; } = default!;
    public virtual Ride Ride { get; set; } = default!;
    public virtual ProveArrived ProveArrived { get; set; } = default!;
    public virtual ComplainArrived ComplainArrived { get; set; } = default!;

    public Transaction(string from, string to, RideRequest rideRequest)
    {
        From = from;
        To = to;
        RideRequest = rideRequest;
    }

    public Transaction(string from, string to, RideOffer rideOffer)
    {
        From = from;
        To = to;
        RideOffer = rideOffer;
    }

    public Transaction(string from, string to, Ride ride)
    {
        From = from;
        To = to;
        Ride = ride;
    }

    public Transaction(string from, string to, ProveArrived proveArrived)
    {
        From = from;
        To = to;
        ProveArrived = proveArrived;
    }

    public Transaction(string from, string to, ComplainArrived complainArrived)
    {
        From = from;
        To = to;
        ComplainArrived = complainArrived;
    }

    public Transaction()
    {

    }

}
