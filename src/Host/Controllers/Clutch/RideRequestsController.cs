using NodeClutchGateway.Application.Clutch.RideOffer;
using NodeClutchGateway.Application.Clutch.RideReuqest;
using NodeClutchGateway.Application.Identity.Users;

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

    [HttpGet("{id:guid}/rideoffers")]
    [OpenApiOperation("Get a ride request's offers.", "")]
    public async Task<List<RideOfferDto>> GetRideOffersByRideRequestTransactionId(Guid id, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetRideOffersByRideRequestTransactionId()
        {
            RideRequestTransactionId = id
        });
    }
}