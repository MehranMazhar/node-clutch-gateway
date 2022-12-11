using NodeClutchGateway.Application.Blockchain;

namespace NodeClutchGateway.Application.Clutch.RideOffer;
public class CreateRideOffer : IRequest<bool>
{
    public double SourceLocation { get; set; }
    public double DestinationLocation { get; set; }
    public double Fare { get; set; }
    public int ExpireInMintue { get; set; }
}

public class CreateRideRequestValidator : CustomValidator<CreateRideOffer>
{
    public CreateRideRequestValidator()
    {
        RuleFor(p => p.SourceLocation)
       .NotEmpty();

        RuleFor(p => p.DestinationLocation)
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
        _blockchainService.AddRideRequest(request.SourceLocation, request.DestinationLocation, request.Fare, request.ExpireInMintue);
        return Task.FromResult(true);
    }
}
