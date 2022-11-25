using NodeClutchGateway.Application.Ride.Reuqest;

namespace NodeClutchGateway.Host.Controllers.Ride;

public class RideRequestController : VersionNeutralApiController
{
    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.RideRequests)]
    [OpenApiOperation("Create a ride request by passenger.", "")]
    public async Task<RideResponse> RideRequest(RideRequest request)
    {
        return await Mediator.Send(request);
    }
}


