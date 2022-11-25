using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Application.Ride.Reuqest;
public class RideRequest : IRequest<RideResponse>
{
    public double SourceLocation { get; set; }
    public double DestinationLocation { get; set; }
    public DateTime Expire { get; set; }
}

public class RideRequestValidator : CustomValidator<RideRequest>
{
    public RideRequestValidator()
    {
        RuleFor(p => p.SourceLocation)
       .NotEmpty();

        RuleFor(p => p.DestinationLocation)
       .NotEmpty();
    }
}
