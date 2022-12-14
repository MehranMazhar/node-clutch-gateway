using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using NodeClutchGateway.Application.Blockchain;
using NodeClutchGateway.Application.Clutch.RideOffer;
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

    #region Public
    public void MineBlock(string minerAddress)
    {
        string cacheKey = TransactionCacheKey();
        var transactions = _cache.Get<List<Transaction>>(cacheKey) ?? new List<Transaction>();
        if (transactions.Count == 0)
            return;

        var lastBlock = GetLastBlock();
        if (lastBlock?.ParentBlockId == null)
            return;

        AddBlock(transactions, lastBlock);
        _cache.Remove(cacheKey);
    }

    public void AddRideRequest(double sourceLocation, double destinationLocation, double fare, int expireInMintue)
    {
        var userId = _currentUser.GetUserId();
        var rideRequest = new RideRequest(sourceLocation, destinationLocation, fare, DateTime.Now.AddMinutes(expireInMintue));
        var transaction = new Transaction(userId.ToString(), userId.ToString(), rideRequest);

        string cacheKey = TransactionCacheKey();
        var transactions = _cache.Get<List<Transaction>>(cacheKey) ?? new List<Transaction>();
        var tUser = transactions.FirstOrDefault(q => q.From == userId.ToString());
        if (tUser != null)
            return;

        transactions.Add(transaction);
        _cache.Set(cacheKey, transactions, TimeSpan.FromMinutes(expireInMintue));
    }

    public void AddRideOffer(Guid rideRequestTransactionId, double fare, int expireInMintue)
    {
        var rideRequest = GetRideRequest(rideRequestTransactionId);
        if (rideRequest == null)
            throw new NotFoundException(string.Format("Ride Requests Not Found"));

        var userId = _currentUser.GetUserId();
        var rideOffer = new RideOffer(fare, DateTime.Now.AddMinutes(expireInMintue), rideRequest.Id);
        var transaction = new Transaction(userId.ToString(), userId.ToString(), rideOffer);

        string cacheKey = TransactionCacheKey();
        var transactions = _cache.Get<List<Transaction>>(cacheKey) ?? new List<Transaction>();
        transactions.Add(transaction);
        _cache.Set(cacheKey, transactions, TimeSpan.FromMinutes(expireInMintue));
    }

    public List<RideRequestDto> GetRideRequest()
    {
        var rideRequests = GetRideRequestsDomain();
        if (rideRequests == null)
            throw new NotFoundException(string.Format("Ride Requests Not Found"));

        return rideRequests.Select(r => new RideRequestDto()
        {
            SourceLocation = r.SourceLocation,
            DestinationLocation = r.DestinationLocation,
            Fare = r.Fare,
            ExpireOn = r.ExpireOn,
            TransactionId = r.TransactionId,
        }).ToList();
    }

    public List<RideOfferDto> GetRideOffers()
    {
        var rideOffers = GetRideOffersDomain();
        if (rideOffers == null)
            throw new NotFoundException(string.Format("Ride offers Not Found"));

        return rideOffers.Select(r => new RideOfferDto()
        {
            ExpireOn = r.ExpireOn,
            Fare = r.Fare,
            RideRequestTransactionId = r.RideRequest.TransactionId,
        }).ToList();
    }

    public List<RideOfferDto> GetRideOffers(Guid rideRequestTransactionId)
    {
        var rideOffers = GetRideOffersDomain(rideRequestTransactionId);
        if (rideOffers == null)
            throw new NotFoundException(string.Format("Ride offers Not Found"));

        return rideOffers.Select(r => new RideOfferDto()
        {
            ExpireOn = r.ExpireOn,
            Fare = r.Fare,
            RideRequestTransactionId = r.RideRequest.TransactionId,
        }).ToList();
    }
    #endregion

    #region Private
    private Block? GetLastBlock()
    {
        return _context.Blocks.OrderBy(q => q.Id).LastOrDefault();
    }

    private string TransactionCacheKey()
    {
        return _cacheKeys.GetCacheKey("Transactions", "Pending", includeTenantId: false);
    }

    private void AddBlock(List<Transaction> transactions, Block lastBlock)
    {
        var block = new Block(lastBlock.Id, transactions);
        _context.Add(block);
        _context.SaveChanges();
    }

    private List<RideRequest> GetRideRequestsDomain()
    {
        return _context.RideRequests.ToList();
    }

    private List<RideOffer> GetRideOffersDomain()
    {
        return _context.RideOffers.Include(q => q.RideRequest).ToList();
    }

    private List<RideOffer> GetRideOffersDomain(Guid rideRequestTransactionId)
    {
        return _context.RideOffers.Include(c => c.RideRequest)
            .Where(q => q.RideRequest.TransactionId == rideRequestTransactionId)
            .ToList();

    }

    private RideRequest? GetRideRequest(Guid transactionId)
    {
        return _context.RideRequests.Where(q => q.TransactionId == transactionId).FirstOrDefault();
    }

    #endregion
}
