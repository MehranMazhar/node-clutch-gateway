using Finbuckle.MultiTenant;
using NodeClutchGateway.Application.Common.Events;
using NodeClutchGateway.Application.Common.Interfaces;
using NodeClutchGateway.Domain.Catalog;
using NodeClutchGateway.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NodeClutchGateway.Domain.Blockchain;

namespace NodeClutchGateway.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(currentTenant, options, currentUser, serializer, dbSettings, events)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Block> Blocks => Set<Block>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<RideRequest> RideRequests => Set<RideRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(SchemaNames.Catalog);
    }
}