// using System;
// using Mang.Application.Contract.Tasks;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.DependencyInjection;
// using Game.Common.Log;
// using Game.Common.Util;
// using Mang.Infrastructure.TaskScheduler;
//
// namespace Mang.Web.Extension.Middleware.Pipeline
// {
//     public static class Quartz
//     {
//         public static void UseQuartzJob(this IApplicationBuilder app,
//             IServiceProvider serviceProvider)
//         {
//             if (app == null) throw new ArgumentNullException(nameof(app));
//
//             if (!Appsettings.app("Middleware", "Job", "Enabled").ToBool()) return;
//
//             ITaskService taskService = serviceProvider.GetService<ITaskService>();
//
//             ISchedulerCenter schedulerCenter = serviceProvider.GetService<ISchedulerCenter>();
//             try
//             {
//                 var taskDtos = taskService.GetAll().Result;
//                 foreach (var task in taskDtos)
//                 {
//                     if (task.IsEnable)
//                     {
//                         var success = schedulerCenter.AddAsync(task).Result;
//                         if (success)
//                         {
//                             Console.WriteLine($"QuartzNetJob{task.Name}启动成功！");
//                         }
//                     }
//                 }
//             }
//             catch (Exception ex)
//             {
//                 LogHelper.Error(ex, "定时任务UseQuartzJob启动失败" + ex.Message);
//             }
//         }
//     }
// }