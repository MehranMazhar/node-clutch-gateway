using Microsoft.EntityFrameworkCore;
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
