using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Domain.Blockchain;
public class RideOffer : AuditableEntity, IAggregateRoot
{
    public double Fare { get; private set; }
    public DateTime ExpireOn { get; private set; }

    public Guid TransactionId { get; private set; }
    public virtual Transaction Transaction { get; private set; }

    public Guid RideRequestTransactionId { get; private set; }
    public virtual Transaction RideRequestTransaction { get; private set; }

    public RideOffer(double fare, DateTime expireOn, Guid rideRequestTransactionId)
    {
        Fare = fare;
        ExpireOn = expireOn;
        RideRequestTransactionId = rideRequestTransactionId;
    }
}
