using Microsoft.IdentityModel.Clients.ActiveDirectory;
using NodeClutchGateway.Application.Blockchain;
using NodeClutchGateway.Application.Clutch.RideReuqest;
using NodeClutchGateway.Application.Common.Caching;
using NodeClutchGateway.Application.Common.Exceptions;
using NodeClutchGateway.Application.Common.Interfaces;
using NodeClutchGateway.Domain.Blockchain;
using NodeClutchGateway.Infrastructure.Auth;
using NodeClutchGateway.Infrastructure.Persistence.Context;

namespace NodeClutchGateway.Infrastructure.Blockchain;
public class BlockchainService : IBlockchainService
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;
    private readonly ICacheService _cache;
    private readonly ICacheKeyService _cacheKeys;

    public BlockchainService(ApplicationDbContext context, ICacheService cache, ICurrentUser currentUser, ICacheKeyService cacheKeys)
    {
        _context = context;
        _cache = cache;
        _currentUser = currentUser;
        _cacheKeys = cacheKeys;
    }

    public void AddRideRequest(double sourceLocation, double destinationLocation, double fare, int expireInMintue)
    {
        var userId = _currentUser.GetUserId();
        var rideRequest = new RideRequest(sourceLocation, destinationLocation, fare, DateTime.Now.AddMinutes(expireInMintue));
        var transaction = new Transaction(userId.ToString(), rideRequest);

        string cacheKey = _cacheKeys.GetCacheKey("PendingTransaction:RideRequest", userId);
        _cache.Set(cacheKey, transaction, TimeSpan.FromMinutes(expireInMintue));
    }

    public async Task<RideRequestDto> GetRideRequestAsync()
    {
        var userId = _currentUser.GetUserId();
        string cacheKey = _cacheKeys.GetCacheKey("PendingTransaction:RideRequest", userId);

        var rideRequestTransaction = await _cache.GetAsync<Transaction>(cacheKey);
        if (rideRequestTransaction == null)
            throw new NotFoundException(string.Format("Not Found", userId));

        return new RideRequestDto()
        {
            SourceLocation = rideRequestTransaction.RideRequest.SourceLocation,
            DestinationLocation = rideRequestTransaction.RideRequest.DestinationLocation,
            Fare = rideRequestTransaction.RideRequest.Fare,
        };

    }

    public void MineBlock(string minerAddress)
    {
        //var block = new Block(_pendingTransactions);
        //_context.Add(block);
        //_context.SaveChanges();        
    }
}
