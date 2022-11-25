using MediatR;
using NodeClutchGateway.Application.Ride.Reuqest;

namespace NodeClutchGateway.Host.Controllers.Ride;

public class RideController : VersionNeutralApiController
{
    [HttpPost]
    [OpenApiOperation("Create a ride request by passenger.", "")]
    public async Task<RideResponse> RideRequest(RideRequest request)
    {
        return await Mediator.Send(request);
    }
}

public class RideRequestHandler : IRequestHandler<RideRequest, RideResponse>
{

    public async Task<RideResponse> Handle(RideRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
