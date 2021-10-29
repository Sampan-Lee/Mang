using System;
using System.Text;
using Mang.Public.Util;
using Mang.Web.Extension.Permission;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Mang.Web.Extension.Dependency
{
    public static class AuthenticationDependency
    {
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // ValidateIssuerSigningKey = true, //是否验证签名,不验证的画可以篡改数据，不安全
                        // IssuerSigningKey =
                        //     new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Appsettings.app("JWT", "Secret"))), //解密的密钥
                        // ValidateIssuer = true, //是否验证发行人，就是验证载荷中的Iss是否对应ValidIssuer参数
                        // ValidIssuer = Appsettings.app("JWT", "Issuer"), //发行人
                        // ValidateAudience = true, //是否验证订阅人，就是验证载荷中的Aud是否对应ValidAudience参数
                        // ValidAudience = Appsettings.app("JWT", "Audience"), //订阅人
                        // ValidateLifetime = true, //是否验证过期时间，过期了就拒绝访问
                        // ClockSkew = TimeSpan.Zero, //这个是缓冲过期时间，也就是说，即使我们配置了过期时间，这里也要考虑进去，过期时间+缓冲，默认好像是7分钟，你可以直接设置为0
                        // RequireExpirationTime = true,
                    };
                });
        }

        public static void AddPolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
                {
                    options.AddPolicy("FinishRegister",
                        policy => policy.RequireClaim("IsFinishRegister", "true")
                    );
                    options.AddPolicy("IsVip",
                        policy => policy.RequireClaim("IsVip", "true"));
                }
            );
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddSingleton<IAuthorizationMiddlewareResultHandler,
                PermissionAuthorizationMiddlewareResultHandler>();
        }
    }
}