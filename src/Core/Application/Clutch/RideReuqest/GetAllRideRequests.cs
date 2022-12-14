using NodeClutchGateway.Application.Blockchain;

namespace NodeClutchGateway.Application.Clutch.RideReuqest;
public class GetAllRideRequests : IRequest<List<RideRequestDto>>
{
}

public class GetAllRideRequestsHandler : IRequestHandler<GetAllRideRequests, List<RideRequestDto>>
{
    private readonly IBlockchainService _blockchainService;

    public GetAllRideRequestsHandler(IBlockchainService blockchainService)
    {
        _blockchainService = blockchainService;
    }

    public Task<List<RideRequestDto>> Handle(GetAllRideRequests request, CancellationToken cancellationToken)
    {
        var rideReuests = _blockchainService.GetRideRequest();
        return Task.FromResult(rideReuests);

    }
}
