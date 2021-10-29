using System;
using Microsoft.Extensions.DependencyInjection;

namespace Mang.Web.Extension.Dependency
{
    public static class CorsDependency
    {
        public static void AddCorsSetup(this IServiceCollection services)
        {
            //跨域信息的配置
            services.AddCors(c =>
            {
                //允许任意跨域请求，也要配置中间件
                c.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Content-Disposition");
                });
            });
        }
    }
}