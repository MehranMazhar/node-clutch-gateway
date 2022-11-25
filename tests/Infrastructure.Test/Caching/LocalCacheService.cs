using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;

namespace Infrastructure.Test.Caching;

public class LocalCacheService : CacheService<NodeClutchGateway.Infrastructure.Caching.LocalCacheService>
{
    protected override NodeClutchGateway.Infrastructure.Caching.LocalCacheService CreateCacheService() =>
        new(
            new MemoryCache(new MemoryCacheOptions()),
            NullLogger<NodeClutchGateway.Infrastructure.Caching.LocalCacheService>.Instance);
}