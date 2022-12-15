using NodeClutchGateway.Application.Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Application.Clutch.Ride;
public class GetRideOfferByTransactionId : IRequest<RideDto>
{
    public DefaultIdType RideRequestTransactionId { get; set; }
    public DefaultIdType RideOfferTransactionId { get; set; }
}

public class GetRideOfferByTransactionIdHandler : IRequestHandler<GetRideOfferByTransactionId, RideDto>
{
    private readonly IBlockchainService _blockchainService;
    public GetRideOfferByTransactionIdHandler(IBlockchainService blockchainService)
    {
        _blockchainService = blockchainService;
    }

    public Task<RideDto> Handle(GetRideOfferByTransactionId request, CancellationToken cancellationToken)
    {
        var result = _blockchainService.GetRide(rideOfferTransactionId: request.RideOfferTransactionId);
        return Task.FromResult(result);
    }
}
