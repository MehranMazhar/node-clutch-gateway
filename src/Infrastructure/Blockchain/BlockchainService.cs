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

        string cacheKey = RideRequestsKey();

        var transactions = _cache.Get<List<Transaction>>(cacheKey) ?? new List<Transaction>();
        var tUser = transactions.FirstOrDefault(q => q.From == userId.ToString());
        if (tUser != null)
            return;

        transactions.Add(transaction);
        _cache.Set(cacheKey, transactions, TimeSpan.FromMinutes(expireInMintue));
    }

    private string RideRequestsKey()
    {
        return _cacheKeys.GetCacheKey("PendingTransaction", "RideRequests", includeTenantId: false);
    }

    public async Task<List<RideRequestDto>> GetRideRequestAsync()
    {
        var userId = _currentUser.GetUserId();
        string cacheKey = RideRequestsKey();

        var rideRequests = await _cache.GetAsync<List<Transaction>>(cacheKey);
        if (rideRequests == null)
            throw new NotFoundException(string.Format("Ride Requests Not Found", userId));

        return rideRequests.Select(t => new RideRequestDto()
        {
            SourceLocation = t.RideRequest.SourceLocation,
            DestinationLocation = t.RideRequest.DestinationLocation,
            Fare = t.RideRequest.Fare,
        }).ToList();

    }

    public void MineBlock(string minerAddress)
    {
        string cacheKey = RideRequestsKey();
        var transactions = _cache.Get<List<Transaction>>(cacheKey);
        var block = new Block("",transactions);
        //_context.Add(block);
        //_context.SaveChanges();
    }
}
