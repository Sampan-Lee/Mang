using System;
using System.Diagnostics;
using FreeSql;
using FreeSql.Aop;
using Microsoft.Extensions.DependencyInjection;
using Mang.Infrastructure;
using Mang.Infrastructure.Repository;
using Mang.Public.Entity;
using Mang.Public.Util;

namespace Mang.Web.Extension.Dependency
{
    public static class FreeSqlDependency
    {
        public static void AddFreeSql(this IServiceCollection services)
        {
            IFreeSql fsql = new FreeSqlBuilder()
                .UseConnectionString(DataType.MySql,
                    Appsettings.app("ConnectionStrings", "MySqlConnectionString"))
                .UseNoneCommandParameter(true)
                .UseMonitorCommand(cmd => { Trace.WriteLine(cmd.CommandText + ";"); })
                .Build();

            fsql.Aop.CurdAfter += (s, e) =>
            {
                Console.WriteLine(e.Sql);
                if (e.ElapsedMilliseconds > 500)
                {
                    LogHelper.Warning($"Sql执行超时：{e.ElapsedMilliseconds}ms\n{e.Sql}");
                }
                else if (e.CurdType != CurdType.Select)
                {
                    if (e.CurdType == CurdType.Insert)
                    {
                        LogHelper.Info($"MySql：{e.Sql} 自增ID：{e.ExecuteResult}");
                    }
                    else
                    {
                        LogHelper.Info($"MySql：{e.Sql}");
                    }
                }
            };

            services.AddSingleton(fsql);
            services.AddScoped<UnitOfWorkManager>();
            services.AddFreeRepository(filter =>
                filter.Apply<ISoftDeleteEntity>("Deleted", a => a.Deleted == false)
            );
            services.AddScoped(typeof(IRepository<>), typeof(FreeSqlRepository<>));
        }
    }
}