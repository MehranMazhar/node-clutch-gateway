using NodeClutchGateway.Application.Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Application.Clutch.RideAcceptance;
public class CreateRideAcceptance : IRequest<bool>
{
    public Guid RideRequestTransactionId { get; set; }
    public Guid RideOfferTransactionId { get; set; }
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
        var result = _blockchainService.RideAcceptance(rideOfferTransactionId: request.RideOfferTransactionId);
        return Task.FromResult(result);
    }
}
