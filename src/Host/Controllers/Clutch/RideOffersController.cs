using NodeClutchGateway.Application.Clutch.RideOffer;

namespace NodeClutchGateway.Host.Controllers.Clutch;
public class RideOffersController : VersionNeutralApiController
{
    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.RideOffer)]
    [OpenApiOperation("Create a ride offer by Driver.", "")]
    public async Task<bool> CreateOfferOffer(CreateRideOffer request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [OpenApiOperation("Get all ride offers.", "")]
    public async Task<bool> GetAllRideOffers(CreateRideOffer request)
    {
        return await Mediator.Send(request);
    }
}