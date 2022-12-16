using NodeClutchGateway.Application.Blockchain;

namespace NodeClutchGateway.Application.Clutch.Ride;
public class ComplainArrivedRequest : IRequest<bool>
{
    public DefaultIdType RideRequestTransactionId { get; set; }
    public DefaultIdType RideOfferTransactionId { get; set; }
    public DefaultIdType RideTransactionId { get; set; }
}

public class ComplainArrivedRequestHandler : IRequestHandler<ComplainArrivedRequest, bool>
{
    private readonly IBlockchainService _blockchainService;

    public ComplainArrivedRequestHandler(IBlockchainService blockchainService)
    {
        _blockchainService = blockchainService;
    }

    public Task<bool> Handle(ComplainArrivedRequest request, CancellationToken cancellationToken)
    {
        _blockchainService.ComplainArrived(rideTransactionId: request.RideTransactionId);
        return Task.FromResult(true);
    }
}
