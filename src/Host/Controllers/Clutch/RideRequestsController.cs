using NodeClutchGateway.Application.Clutch.RideAcceptance;
using NodeClutchGateway.Application.Clutch.RideOffer;
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

    [HttpPost("{rideRequestTransactionId:guid}/rideoffers")]
    [MustHavePermission(FSHAction.Create, FSHResource.RideOffer)]
    [OpenApiOperation("Create a ride offer by Driver.", "")]
    public async Task<bool> CreateRideOffer(Guid rideRequestTransactionId, CreateRideOffer request)
    {
        return await Mediator.Send(new CreateRideOffer()
        {
            RideRequestTransactionId = rideRequestTransactionId,
            ExpireInMintue = request.ExpireInMintue,
            Fare = request.Fare,
        });
    }

    [HttpGet("{rideRequestTransactionId:guid}/rideoffers")]
    [OpenApiOperation("Get a ride request's offers.", "")]
    public async Task<List<RideOfferDto>> GetRideOffersByRideRequestTransactionId(Guid rideRequestTransactionId)
    {
        return await Mediator.Send(new GetRideOffersByRideRequestTransactionId()
        {
            RideRequestTransactionId = rideRequestTransactionId
        });
    }

    [HttpPost("{rideRequestTransactionId:guid}/rideoffers/{rideOfferTransactionId:guid}/RideAcceptance")]
    [MustHavePermission(FSHAction.Create, FSHResource.RideAcceptance)]
    [OpenApiOperation("Add ride acceptance.", "")]
    public async Task<bool> CreateRideAcceptance(Guid rideRequestTransactionId, Guid rideOfferTransactionId)
    {
        return await Mediator.Send(new CreateRideAcceptance()
        {
            RideRequestTransactionId = rideRequestTransactionId,
            RideOfferTransactionId = rideOfferTransactionId,
        });
    }
}