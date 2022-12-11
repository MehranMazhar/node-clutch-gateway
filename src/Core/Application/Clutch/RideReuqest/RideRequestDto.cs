using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Application.Clutch.RideReuqest;
public class RideRequestDto : IDto
{
    public double SourceLocation { get; set; }
    public double DestinationLocation { get; set; }
    public double Fare { get; set; }
    public DateTime ExpireOn { get; set; }
    public Guid TransactionId { get; set; }
}
