using NodeClutchGateway.Domain.Catalog;

namespace NodeClutchGateway.Domain.Blockchain;
public class Transaction : AuditableEntity, IAggregateRoot
{
    public string From { get; private set; }
    public string To { get; private set; }
    public double Amount { get; set; }
    public Guid BlockId { get; private set; }
    public virtual Block Block { get; private set; } = default!;
    public virtual RideRequest RideRequest { get; private set; }

    public Transaction(string from, string to, double amount)
    {
        From = from;
        To = to;        
        Amount = amount;
    }

    public Transaction(string from, RideRequest rideRequest)
    {
        From = from;        
        RideRequest = rideRequest;
    }

    public Transaction()
    {

    }

}
