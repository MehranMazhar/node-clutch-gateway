using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Application.Clutch.RideOffer;
public class RideOfferDto : IDto
{
    public DateTime ExpireOn { get; set; }
    public double Fare { get; set; }
    public Guid RideRequestTransactionId { get; set; }
}
