using NodeClutchGateway.Application.Clutch.RideOffer;

namespace NodeClutchGateway.Host.Controllers.Clutch;
public class RideOffersController : VersionNeutralApiController
{
    [HttpGet]
    [OpenApiOperation("Get all ride offers.", "")]
    public async Task<List<RideOfferDto>> GetAllRideOffers()
    {
        return await Mediator.Send(new GetAllRideOffers());
    }   
}