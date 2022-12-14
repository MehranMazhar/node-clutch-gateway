using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Domain.Blockchain;
public class Ride : AuditableEntity, IAggregateRoot
{
    public Guid TransactionId { get; set; }
    public virtual Transaction Transaction { get; set; } = default!;

    public Guid RideOfferId { get; set; }
    public virtual RideOffer RideOffer { get; set; } = default!;

    public Ride(Guid rideOfferId)
    {
        RideOfferId = rideOfferId;
    }
}
