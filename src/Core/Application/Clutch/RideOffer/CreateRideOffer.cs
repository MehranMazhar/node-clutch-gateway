using NodeClutchGateway.Application.Blockchain;

namespace NodeClutchGateway.Application.Clutch.RideOffer;
public class CreateRideOffer : IRequest<bool>
{
    public Guid? RideRequestTransactionId { get; set; }
    public double Fare { get; set; }
    public int ExpireInMintue { get; set; }
}

public class CreateRideRequestValidator : CustomValidator<CreateRideOffer>
{
    public CreateRideRequestValidator()
    {
        RuleFor(p => p.Fare)
       .NotEmpty();

        RuleFor(p => p.ExpireInMintue)
       .NotEmpty();
    }
}

public class CreateRideRequestHandler : IRequestHandler<CreateRideOffer, bool>
{
    private readonly IBlockchainService _blockchainService;

    public CreateRideRequestHandler(IBlockchainService blockchainService)
    {
        _blockchainService = blockchainService;
    }

    public Task<bool> Handle(CreateRideOffer request, CancellationToken cancellationToken)
    {
        _blockchainService.AddRideOffer(request.RideRequestTransactionId.Value, request.Fare, request.ExpireInMintue);
        return Task.FromResult(true);
    }
}
