using NodeClutchGateway.Application.Blockchain;

namespace NodeClutchGateway.Application.Clutch.Ride;
public class ProveArrivedRequest : IRequest<bool>
{
    public DefaultIdType RideRequestTransactionId { get; set; }
    public DefaultIdType RideOfferTransactionId { get; set; }
    public DefaultIdType RideTransactionId { get; set; }
}

public class ProveArrivedRequestHandler : IRequestHandler<ProveArrivedRequest, bool>
{
    private readonly IBlockchainService _blockchainService;

    public ProveArrivedRequestHandler(IBlockchainService blockchainService)
    {
        _blockchainService = blockchainService;
    }

    public Task<bool> Handle(ProveArrivedRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
