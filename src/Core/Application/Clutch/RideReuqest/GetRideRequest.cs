using NodeClutchGateway.Application.Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Application.Clutch.RideReuqest;
public class GetRideRequest : IRequest<List<RideRequestDto>>
{
}

public class GetRideRequestHandle : IRequestHandler<GetRideRequest, List<RideRequestDto>>
{
    private readonly IBlockchainService _blockchainService;

    public GetRideRequestHandle(IBlockchainService blockchainService)
    {
        _blockchainService = blockchainService;
    }

    public async Task<List<RideRequestDto>> Handle(GetRideRequest request, CancellationToken cancellationToken)
    {
        return await _blockchainService.GetRideRequestAsync();

    }
}
