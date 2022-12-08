using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Application.RideReuqest;
public class CreateRideRequest : IRequest<CreateRideResponse>
{
    public double SourceLocation { get; set; }
    public double DestinationLocation { get; set; }
    public int ExpireInMintue { get; set; }
}

public class CreateRideRequestValidator : CustomValidator<CreateRideRequest>
{
    public CreateRideRequestValidator()
    {
        RuleFor(p => p.SourceLocation)
       .NotEmpty();

        RuleFor(p => p.DestinationLocation)
       .NotEmpty();

        RuleFor(p => p.ExpireInMintue)
       .NotEmpty();
    }
}

public class RideRequestHandler : IRequestHandler<CreateRideRequest, CreateRideResponse>
{

    public async Task<CreateRideResponse> Handle(CreateRideRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}