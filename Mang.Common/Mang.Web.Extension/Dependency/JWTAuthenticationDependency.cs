using System.Text;
using Mang.Public.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Mang.Web.Extension.Dependency
{
    public static class JWTAuthenticationDependency
    {
        public static void AddIdentityServerAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = true,
                            ValidAudience = Appsettings.app("JWT", "Audience"),
                            ValidateIssuer = true,
                            ValidIssuer = Appsettings.app("JWT", "Issuer"),
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey =
                                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Appsettings.app("JWT", "Secret"))),
                            ValidateLifetime = true,
                        };
                    }
                );
        }
    }
}