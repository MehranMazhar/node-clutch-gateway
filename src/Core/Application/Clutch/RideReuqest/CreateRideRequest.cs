using NodeClutchGateway.Application.Blockchain;
using NodeClutchGateway.Domain.Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeClutchGateway.Application.Clutch.RideReuqest;
public class CreateRideRequest : IRequest<CreateRideResponse>
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

        RuleFor(p => p.ExpireInMintue)
       .NotEmpty();
    }
}

public class CreateRideRequestHandler : IRequestHandler<CreateRideRequest, CreateRideResponse>
{
    private readonly IBlockchainService _blockchainService;

    public CreateRideRequestHandler(IBlockchainService blockchainService)
    {
        _blockchainService = blockchainService;
    }

    public Task<CreateRideResponse> Handle(CreateRideRequest request, CancellationToken cancellationToken)
    {
        _blockchainService.AddRideRequest(request.SourceLocation, request.DestinationLocation, request.Fare, request.ExpireInMintue);

        var rs = new CreateRideResponse()
        {
            Success = true,
        };

        return Task.FromResult(rs);
    }
}