using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Application.Clutch.Ride;
public class RideDto : IDto
{
    public Guid TransactionId { get; set; }
}
