using NodeClutchGateway.Application.Clutch.Ride;
using NodeClutchGateway.Application.Clutch.RideOffer;
using NodeClutchGateway.Application.Clutch.RideReuqest;
using NodeClutchGateway.Domain.Blockchain;

namespace NodeClutchGateway.Application.Blockchain;
public interface IBlockchainService : ITransientService
{
    void MineBlock(string minerAddress);
    void AddRideOffer(Guid rideRequestTransactionId, double fare, int expireInMintue);
    void AddRideRequest(double sourceLocation, double destinationLocation, double fare, int expireInMintue);
    List<RideOfferDto> GetRideOffers();
    List<RideOfferDto> GetRideOffers(DefaultIdType rideRequestTransactionId);
    List<RideRequestDto> GetRideRequest();
    void RideAcceptance(DefaultIdType rideOfferTransactionId);
    RideDto GetRide(DefaultIdType rideOfferTransactionId);
    void ProveArrived(DefaultIdType rideTransactionId);
    void ComplainArrived(DefaultIdType rideTransactionId);
}
