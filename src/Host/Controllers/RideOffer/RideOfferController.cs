using NodeClutchGateway.Application.Clutch.RideOffer;

namespace NodeClutchGateway.Host.Controllers.RideOffer;
public class RideOfferController : VersionNeutralApiController
{
    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.RideOffer)]
    [OpenApiOperation("Create a ride offer by Driver.", "")]
    public async Task<bool> CreateOfferOffer(CreateRideOffer request)
    {
        return await Mediator.Send(request);
    }
}