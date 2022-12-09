using NodeClutchGateway.Application.Clutch.RideReuqest;
using NodeClutchGateway.Domain.Blockchain;

namespace NodeClutchGateway.Application.Blockchain;
public interface IBlockchainService : ITransientService
{
    void AddRideRequest(double sourceLocation, double destinationLocation, double fare, int expireInMintue);
    Task<RideRequestDto> GetRideRequestAsync();
    void MineBlock(string minerAddress);
}
