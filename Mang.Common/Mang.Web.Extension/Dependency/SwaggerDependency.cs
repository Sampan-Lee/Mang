using System;
using System.Collections.Generic;
using System.IO;
using Mang.Public.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Mang.Web.Extension.Dependency
{
    public static class SwaggerDependency
    {
        /// <summary>
        /// 注入 Swagger 服务到容器内
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(this IServiceCollection services)
        {
            var title = Appsettings.app("Application", "Title");

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}(注意两者之间是一个【空格】)",
                    Name = "Authorization", //jwt默认的参数名称
                    In = ParameterLocation.Header, //jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                };

                var basePath = AppContext.BaseDirectory;
                var controllerXmlPath = Path.Combine(basePath, $"{Appsettings.app("Application", "Name")}.xml");
                options.IncludeXmlComments(controllerXmlPath, true);

                var contractXmlPath = Path.Combine(basePath, "Mang.Application.Contract.xml");
                options.IncludeXmlComments(contractXmlPath);

                options.AddSecurityDefinition("Bearer", securitySchema);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
            });
        }

        /// <summary>
        /// Api接口版本 自定义
        /// </summary>
        public enum ApiVersions
        {
            /// <summary>
            /// V1 版本
            /// </summary>
            V1 = 1,
        }
    }
}