using System;
using Microsoft.Extensions.DependencyInjection;
using Mang.Infrastructure.TaskScheduler;
using Mang.Public.Extension;
using Mang.Public.Util;

namespace Mang.Web.Extension.Dependency
{
    public static class TaskDependency
    {
        public static void AddTaskSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //ITaskJobService 构造需要 SchedulerCenter 在这里提前注入
            services.AddSingleton<ISchedulerCenter>();

            //任务是否需要开启
            if (!Appsettings.app("Middleware", "Job", "Enabled").ToBool()) return;

            //services.AddTransient<AppointmentResourceJob>();
            //services.AddTransient<AppointmentOrderTimeOutJob>();
        }
    }
}