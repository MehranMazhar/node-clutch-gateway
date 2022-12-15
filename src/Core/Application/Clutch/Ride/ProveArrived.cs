using NodeClutchGateway.Application.Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Application.Clutch.Ride;
public class ProveArrived : IRequest<bool>
{
    public DefaultIdType RideRequestTransactionId { get; set; }
    public DefaultIdType RideOfferTransactionId { get; set; }
    public DefaultIdType RideTransactionId { get; set; }
}

public class ProveArrivedHandler : IRequestHandler<ProveArrived, bool>
{
    private readonly IBlockchainService _blockchainService;
    public ProveArrivedHandler(IBlockchainService blockchainService)
    {
        _blockchainService = blockchainService;
    }

    public Task<bool> Handle(ProveArrived request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
