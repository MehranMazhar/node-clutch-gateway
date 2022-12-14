﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodeClutchGateway.Domain.Blockchain;

namespace NodeClutchGateway.Infrastructure.Persistence.Configuration;
public class BlockConfig : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder
            .ToTable("Blocks", SchemaNames.Blockchain);
    }
}

public class TransactionConfig : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder
            .ToTable("Transactions", SchemaNames.Blockchain);
    }
}

public class RideRequestConfig : IEntityTypeConfiguration<RideRequest>
{
    public void Configure(EntityTypeBuilder<RideRequest> builder)
    {
        builder
            .ToTable("RideRequests", SchemaNames.Blockchain);
    }
}

public class RideOfferConfig : IEntityTypeConfiguration<RideOffer>
{
    public void Configure(EntityTypeBuilder<RideOffer> builder)
    {
        builder
            .ToTable("RideOffers", SchemaNames.Blockchain);


        builder.HasOne(a => a.RideRequest)
            .WithMany(a => a.RideOffers)
            .HasForeignKey(a => a.RideRequestId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
