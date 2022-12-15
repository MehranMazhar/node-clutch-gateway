using NodeClutchGateway.Application.Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Application.Clutch.Ride;
public class CreateRideAcceptance : IRequest<bool>
{
    public DefaultIdType RideRequestTransactionId { get; set; }
    public DefaultIdType RideOfferTransactionId { get; set; }
}

public class CreateRideAcceptanceHandler : IRequestHandler<CreateRideAcceptance, bool>
{
    private readonly IBlockchainService _blockchainService;
    public CreateRideAcceptanceHandler(IBlockchainService blockchainService)
    {
        _blockchainService = blockchainService;
    }

    public Task<bool> Handle(CreateRideAcceptance request, CancellationToken cancellationToken)
    {
        _blockchainService.RideAcceptance(rideOfferTransactionId: request.RideOfferTransactionId);
        return Task.FromResult(true);
    }
}
