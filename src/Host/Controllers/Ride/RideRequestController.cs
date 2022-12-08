using NodeClutchGateway.Application.Clutch.RideReuqest;

namespace NodeClutchGateway.Host.Controllers.Ride;

public class RideRequestController : VersionNeutralApiController
{
    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.RideRequests)]
    [OpenApiOperation("Create a ride request by passenger.", "")]
    public async Task<CreateRideResponse> RideRequest(CreateRideRequest request)
    {
        return await Mediator.Send(request);
    }
}


