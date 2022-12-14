using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Domain.Blockchain;
public class RideOffer : AuditableEntity, IAggregateRoot
{
    public double Fare { get; set; }
    public DateTime ExpireOn { get; set; }

    public Guid TransactionId { get; set; }
    public virtual Transaction Transaction { get; set; }

    public Guid RideRequestId { get; set; }
    public virtual RideRequest RideRequest { get; set; }

    public RideOffer(double fare, DateTime expireOn, Guid rideRequestId)
    {
        Fare = fare;
        ExpireOn = expireOn;
        RideRequestId = rideRequestId;
    }

    public RideOffer()
    {

    }
}
