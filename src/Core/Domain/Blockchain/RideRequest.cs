using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Domain.Blockchain;
public class RideRequest : AuditableEntity, IAggregateRoot
{
    public double SourceLocation { get; private set; }
    public double DestinationLocation { get; private set; }
    public double Fare { get; private set; }
    public DateTime ExpireOn { get; private set; }
    public Guid TransactionId { get; private set; }
    public virtual Transaction Transaction { get; private set; }
    public virtual ICollection<RideOffer> RideOffers { get; set; }

    public RideRequest(double sourceLocation, double destinationLocation, double fare, DateTime expireOn)
    {
        SourceLocation = sourceLocation;
        DestinationLocation = destinationLocation;
        Fare = fare;
        ExpireOn = expireOn;
    }
}
