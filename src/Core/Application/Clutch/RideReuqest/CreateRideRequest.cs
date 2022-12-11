using NodeClutchGateway.Application.Blockchain;

namespace NodeClutchGateway.Application.Clutch.RideReuqest;
public class CreateRideRequest : IRequest<bool>
{
    public double SourceLocation { get; set; }
    public double DestinationLocation { get; set; }
    public double Fare { get; set; }
    public int ExpireInMintue { get; set; }
}

public class CreateRideRequestValidator : CustomValidator<CreateRideRequest>
{
    public CreateRideRequestValidator()
    {
        RuleFor(p => p.SourceLocation)
       .NotEmpty();

        RuleFor(p => p.DestinationLocation)
       .NotEmpty();

        RuleFor(p => p.Fare)
       .NotEmpty();

        RuleFor(p => p.ExpireInMintue)
       .NotEmpty();
    }
}

public class CreateRideRequestHandler : IRequestHandler<CreateRideRequest, bool>
{
    private readonly IBlockchainService _blockchainService;

    public CreateRideRequestHandler(IBlockchainService blockchainService)
    {
        _blockchainService = blockchainService;
    }

    public Task<bool> Handle(CreateRideRequest request, CancellationToken cancellationToken)
    {
        _blockchainService.AddRideRequest(request.SourceLocation, request.DestinationLocation, request.Fare, request.ExpireInMintue);
        return Task.FromResult(true);
    }
}