using NodeClutchGateway.Application.Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Application.Clutch.RideOffer;
public class GetRideOffersByRideRequestTransactionId : IRequest<List<RideOfferDto>>
{
    public Guid RideRequestTransactionId { get; set; }
}

public class GetRideOffersByRideRequestTransactionIdHandler : IRequestHandler<GetRideOffersByRideRequestTransactionId, List<RideOfferDto>>
{
    private readonly IBlockchainService _blockchainService;
    public GetRideOffersByRideRequestTransactionIdHandler(IBlockchainService blockchainService)
    {
        _blockchainService = blockchainService;
    }

    public Task<List<RideOfferDto>> Handle(GetRideOffersByRideRequestTransactionId request, CancellationToken cancellationToken)
    {
        var rideOffers = _blockchainService.GetRideOffers(request.RideRequestTransactionId);
        return Task.FromResult(rideOffers);
    }
}
