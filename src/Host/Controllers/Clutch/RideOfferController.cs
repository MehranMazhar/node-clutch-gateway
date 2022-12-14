using NodeClutchGateway.Application.Clutch.RideOffer;

namespace NodeClutchGateway.Host.Controllers.Clutch;
public class RideOfferController : VersionNeutralApiController
{
    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.RideOffer)]
    [OpenApiOperation("Create a ride offer by Driver.", "")]
    public async Task<bool> CreateOfferOffer(CreateRideOffer request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.RideOffer)]
    [OpenApiOperation("Create a ride offer by Driver.", "")]
    public async Task<bool> GetOfferOffer(CreateRideOffer request)
    {
        return await Mediator.Send(request);
    }
}