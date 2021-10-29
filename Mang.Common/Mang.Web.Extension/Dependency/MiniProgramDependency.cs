using Mang.Infrastructure.MiniProgram.Auths;
using Microsoft.Extensions.DependencyInjection;

namespace Mang.Web.Extension.Dependency
{
    public static class MiniProgramDependency
    {
        public static void AddMiniProgram(this IServiceCollection service)
        {
            service.AddSingleton<IMiniProgramAuth, MiniProgramAuth>();
        }
    }
}