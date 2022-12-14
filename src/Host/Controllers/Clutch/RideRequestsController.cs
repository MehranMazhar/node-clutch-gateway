using NodeClutchGateway.Application.Clutch.RideReuqest;

namespace NodeClutchGateway.Host.Controllers.Clutch;

public class RideRequestsController : VersionNeutralApiController
{
    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.RideRequests)]
    [OpenApiOperation("Create a ride request by passenger.", "")]
    public async Task<bool> CreateRideRequest(CreateRideRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [AllowAnonymous]
    [OpenApiOperation("Get all ride request", "")]
    public async Task<List<RideRequestDto>> GetAllRideRequests()
    {
        return await Mediator.Send(new GetAllRideRequests());
    }
}