using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using NodeClutchGateway.Application.Blockchain;
using NodeClutchGateway.Application.Clutch.Ride;
using NodeClutchGateway.Application.Clutch.RideOffer;
using NodeClutchGateway.Application.Clutch.RideReuqest;
using NodeClutchGateway.Application.Common.Caching;
using NodeClutchGateway.Application.Common.Exceptions;
using NodeClutchGateway.Application.Common.Interfaces;
using NodeClutchGateway.Domain.Blockchain;
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

        AddPendingTransaction(transaction, expireInMintue);
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

        return rideOffers.Select(RideRequestDto).ToList();
    }

    public List<RideOfferDto> GetRideOffers(Guid rideRequestTransactionId)
    {
        var rideOffers = GetRideOffersDomain(rideRequestTransactionId);
        if (rideOffers == null)
            throw new NotFoundException(string.Format("Ride offers Not Found"));

        return rideOffers.Select(RideRequestDto).ToList();
    }

    public void RideAcceptance(Guid rideOfferTransactionId)
    {
        var userId = _currentUser.GetUserId();

        var rideOffer = GetRideOfferDomainByTransactionId(rideOfferTransactionId);
        if (rideOffer == null)
            throw new NotFoundException(string.Format("Ride offer not found."));

        var rideRequest = rideOffer.RideRequest;
        if (rideRequest == null)
            throw new NotFoundException(string.Format("Ride request not found."));

        if (rideRequest.Transaction.From != userId.ToString())
            throw new ForbiddenException("You are not authorized to access this resource.");

        var rideOffersRequest = _context.RideRequests.Include(c => c.RideOffers).ThenInclude(c => c.Ride).Where(q => q.Id == rideRequest.Id).ToList();
        if (rideOffersRequest.SelectMany(s => s.RideOffers).Any(q => q.Ride != null))
            throw new ForbiddenException("Already ride created!");

        var ride = new Ride(rideOffer.Id);
        var transaction = new Transaction(userId.ToString(), userId.ToString(), ride);
        AddPendingTransaction(transaction);
    }

    public RideDto GetRide(Guid rideOfferTransactionId)
    {
        var rideOffer = GetRideOfferDomainByTransactionId(rideOfferTransactionId);
        if (rideOffer == null)
            throw new NotFoundException(string.Format("Ride offer not found."));

        var rideRequest = rideOffer.RideRequest;
        if (rideRequest == null)
            throw new NotFoundException(string.Format("Ride request not found."));

        var ride = rideOffer.Ride;
        if (ride == null)
            throw new NotFoundException(string.Format("Ride not found."));

        return new RideDto()
        {
            TransactionId = ride.TransactionId,
        };
    }

    public void ProveArrived(Guid rideTransactionId)
    {
        var ride = GetRideDomain(rideTransactionId);
        if (ride == null)
            throw new NotFoundException(string.Format("Ride not found."));

        string userId = _currentUser.GetUserId().ToString();
        string driver = ride.RideOffer.Transaction.From;
        string passenger = ride.RideOffer.RideRequest.Transaction.From;

        if (userId != driver && userId != passenger)
            throw new ForbiddenException(string.Format("Not alowed by sender!:{}", userId));

        int proveArrivedCount = ride.ProveArriveds.Count(q => q.Transaction.From == userId);
        if (driver != passenger)
        {
            if (proveArrivedCount == 1)
                throw new ForbiddenException(string.Format("Prove Arrived has been submited."));
        }
        else
        {
            if (proveArrivedCount == 2)
                throw new ForbiddenException(string.Format("Prove Arrived has been submited."));
        }

        var proveAraived = new ProveArrived(ride.Id);
        var transaction = new Transaction(userId, userId, proveAraived);
        AddPendingTransaction(transaction);
    }

    public void ComplainArrived(Guid rideTransactionId)
    {
        var ride = GetRideDomain(rideTransactionId);
        if (ride == null)
            throw new NotFoundException(string.Format("Ride not found."));

        string userId = _currentUser.GetUserId().ToString();
        string driver = ride.RideOffer.Transaction.From;
        string passenger = ride.RideOffer.RideRequest.Transaction.From;

        if (userId != driver && userId != passenger)
            throw new ForbiddenException(string.Format("Not alowed by sender!:{}", userId));

        int proveArrivedCount = ride.ProveArriveds.Count();
        if (proveArrivedCount != 1)
        {
            throw new ForbiddenException(string.Format("Prove Arrived has been submited."));
        }

        var proveArrived = ride.ProveArriveds.First();
        if (proveArrived.Transaction.From == userId)
            throw new ForbiddenException(string.Format("Prove Arrived has been submited."));

    }

    #endregion

    #region Private
    private void AddPendingTransaction(Transaction transaction, int expireInMintue = 20)
    {
        string cacheKey = TransactionCacheKey();
        var transactions = _cache.Get<List<Transaction>>(cacheKey) ?? new List<Transaction>();
        transactions.Add(transaction);
        _cache.Set(cacheKey, transactions, TimeSpan.FromMinutes(expireInMintue));
    }

    private static RideOfferDto RideRequestDto(RideOffer r)
    {
        return new RideOfferDto()
        {
            ExpireOn = r.ExpireOn,
            Fare = r.Fare,
            RideRequestTransactionId = r.RideRequest.TransactionId,
            TransactionId = r.TransactionId,
        };
    }

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

    private RideOffer? GetRideOfferDomainByTransactionId(Guid rideOfferTransactionId)
    {
        return _context.RideOffers
            .Include(rideOffer => rideOffer.Ride)
            .Include(rideOffer => rideOffer.RideRequest)
            .ThenInclude(rideRequest => rideRequest.Transaction)
            .ThenInclude(rideRequest => rideRequest.RideOffer)
            .ThenInclude(RideOffer => RideOffer.Ride)
            .Where(q => q.TransactionId == rideOfferTransactionId)
            .FirstOrDefault();
    }

    private RideRequest? GetRideRequest(Guid transactionId)
    {
        return _context.RideRequests.Where(q => q.TransactionId == transactionId).FirstOrDefault();
    }

    private Ride? GetRideDomain(Guid rideTransactionId)
    {
        return _context.Rides.Where(q => q.TransactionId == rideTransactionId)
            .Include(c => c.Transaction)
            .Include(c => c.RideOffer).ThenInclude(c => c.Transaction)
            .Include(c => c.RideOffer).ThenInclude(c => c.RideRequest).ThenInclude(c => c.Transaction)
            .Include(c => c.ProveArriveds).ThenInclude(c => c.Transaction)
            .FirstOrDefault();
    }

    #endregion
}
