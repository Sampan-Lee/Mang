using Mang.Public.Constant;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Mang.Web.Extension.Filter;

namespace Mang.Web.Extension.Dependency
{
    public static class MvcDependency
    {
        public static void AddCoreMvc(this IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
                {
                    options.Filters.Add<DuplicateSubmissionActionFilter>(); //重复提交
                    options.Filters.Add<ExceptionHandleFilter>(); //异常处理
                    options.Filters.Add<CustomActionFilter>(); //参数校验,自定义返回结果
                })
                .AddNewtonsoftJson(options =>
                {
                    //忽略循环引用
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //不使用驼峰样式的key 目前是开启的
                    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //设置时间格式
                    options.SerializerSettings.DateFormatString = DateTimeFormat.yyyy_MM_dd_HH_mm_ss;
                });

            //文件上传默认大小不受限制 后续加上大小限制
            services.Configure<FormOptions>(x =>
            {
                x.ValueCountLimit = int.MaxValue;
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
            });
        }
    }
}