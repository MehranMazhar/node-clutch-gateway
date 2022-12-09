using Microsoft.Extensions.DependencyInjection;
using NodeClutchGateway.Application.Common.Interfaces;
using NodeClutchGateway.Infrastructure.Common;

namespace NodeClutchGateway.Infrastructure.RecurringJobs;
internal static class Startup
{
    internal static IServiceCollection AddRecurringBackgroundJobs(this IServiceCollection services)
    {
        services.AddServices(typeof(IRecurringJobInitialization), ServiceLifetime.Transient);

        return services;
    }
}
