using Microsoft.Extensions.DependencyInjection;
using Mang.Infrastructure.DistributedCache;

namespace Mang.Web.Extension.Dependency
{
    public static class DistributedCacheDependency
    {
        public static void AddDistributedCache(this IServiceCollection services)
        {
            services.AddSingleton<IDistributedCache, RedisDistributedCache>();
        }
    }
}