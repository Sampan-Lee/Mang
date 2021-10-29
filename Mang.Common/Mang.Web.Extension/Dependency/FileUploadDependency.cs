using Mang.Infrastructure.DistributedCache;
using Mang.Infrastructure.File;
using Microsoft.Extensions.DependencyInjection;

namespace Mang.Web.Extension.Dependency
{
    public static class IFileUploadDependency
    {
        public static void AddFileUpload(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IFileUploadService), typeof(LocalFileUploadService));
        }
    }
}