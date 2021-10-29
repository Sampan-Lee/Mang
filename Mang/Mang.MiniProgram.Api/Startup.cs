using Autofac;
using Mang.Public.Util;
using Mang.Web.Extension.Dependency;
using Mang.Web.Extension.Middleware;
using Mang.Web.Extension.Middleware.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mang.MiniProgram.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new Appsettings());
            services.AddHttpClient();
            services.AddCoreMvc();
            services.AddJwtAuthentication();
            services.AddPolicyAuthorization();
            services.AddFreeSql();
            services.AddDistributedCache();
            services.AddFileUpload();
            services.AddMiniProgram();
            services.AddMapper();
            services.AddSwagger();
            services.AddCorsSetup();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacDependency>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mang.MiniProgram.Api v1"));
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseNlog();

            app.UseHttpLog();

            app.UseEndpoints(endpoints => { endpoints.MapControllers().RequireAuthorization(); });
        }
    }
}