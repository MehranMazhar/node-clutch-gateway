using MediatR;
using NodeClutchGateway.Application.Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Application.Clutch.RideOffer;
public class GetAllRideOffers : IRequest<List<RideOfferDto>>
{
}

public class GetAllRideOffersHandler : IRequestHandler<GetAllRideOffers, List<RideOfferDto>>
{
    private readonly IBlockchainService _blockchainService;

    public GetAllRideOffersHandler(IBlockchainService blockchainService)
    {
        _blockchainService = blockchainService;
    }

    public Task<List<RideOfferDto>> Handle(GetAllRideOffers request, CancellationToken cancellationToken)
    {
        var rideOffers = _blockchainService.GetRideOffers();
        return Task.FromResult(rideOffers);
    }
}
