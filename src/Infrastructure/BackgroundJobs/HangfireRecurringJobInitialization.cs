using Finbuckle.MultiTenant;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NodeClutchGateway.Application.Blockchain;
using NodeClutchGateway.Application.Common.Interfaces;
using NodeClutchGateway.Infrastructure.Multitenancy;

namespace NodeClutchGateway.Infrastructure.BackgroundJobs;
public class HangfireRecurringJobInitialization : IRecurringJobInitialization
{
    private readonly ILogger<HangfireRecurringJobInitialization> _logger;
    private readonly IJobService _jobService;
    private readonly TenantDbContext _tenantDbContext;
    private readonly IServiceProvider _serviceProvider;
    private readonly ITenantInfo _tenantInfo;

    public HangfireRecurringJobInitialization(ILogger<HangfireRecurringJobInitialization> logger, IJobService jobService, TenantDbContext tenantDbContext, IServiceProvider serviceProvider, ITenantInfo tenantInfo)
    {
        _logger = logger;
        _jobService = jobService;
        _tenantDbContext = tenantDbContext;
        _serviceProvider = serviceProvider;
        _tenantInfo = tenantInfo;
    }

    public async Task InitializeJobsForTenantAsync(CancellationToken cancellationToken)
    {
        foreach (var tenant in await _tenantDbContext.TenantInfo.ToListAsync(cancellationToken))
        {
            InitializeJobsForTenant(tenant);
        }
    }


    private void InitializeJobsForTenant(FSHTenantInfo tenant)
    {
        // First create a new scope
        using var scope = _serviceProvider.CreateScope();

        // Then set current tenant so the right connectionstring is used
        scope.ServiceProvider.GetRequiredService<IMultiTenantContextAccessor>()
            .MultiTenantContext = new MultiTenantContext<FSHTenantInfo>()
            {
                TenantInfo = tenant
            };

        scope.ServiceProvider.GetRequiredService<IRecurringJobInitialization>().InitializeRecurringJobs();
    }

    public void InitializeRecurringJobs()
    {
        _jobService.AddOrUpdate<IBlockchainService>("MineBlock", x => x.MineBlock("s"), () => Cron.MinuteInterval(1), TimeZoneInfo.Utc, "default");
        _logger.LogInformation("All recurring jobs have been initialized.");
    }
}
