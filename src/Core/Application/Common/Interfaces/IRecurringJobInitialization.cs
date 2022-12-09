namespace NodeClutchGateway.Application.Common.Interfaces;

public interface IRecurringJobInitialization
{
    void InitializeRecurringJobs();
    Task InitializeJobsForTenantAsync(CancellationToken cancellationToken);
}