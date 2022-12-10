using NodeClutchGateway.Application.Clutch.RideReuqest;

namespace NodeClutchGateway.Host.Controllers.Ride;

public class RideRequestController : VersionNeutralApiController
{
    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.RideRequests)]
    [OpenApiOperation("Create a ride request by passenger.", "")]
    public async Task<CreateRideResponse> CreateRideRequest(CreateRideRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(FSHAction.Create, FSHResource.RideRequests)]
    [OpenApiOperation("Get pending current ride request by passenger", "")]
    public async Task<List<RideRequestDto>> GetRideRequest()
    {
        return await Mediator.Send(new GetRideRequest());
    }
}