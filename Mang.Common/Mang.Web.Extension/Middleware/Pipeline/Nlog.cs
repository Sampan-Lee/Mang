using Mang.Public.Util;
using Microsoft.AspNetCore.Builder;
using NLog;

namespace Mang.Web.Extension.Middleware
{
    public static class Nlog
    {
        public static void UseNlog(this IApplicationBuilder app)
        {
            LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
            LogManager.Configuration.Variables["configDir"] = Appsettings.app("AppSettings", "LogFilesDir");
            var connectionString = Appsettings.app("ConnectionStrings", "MySqlConnectionString");
            LogManager.Configuration.Variables["connectionString"] = connectionString;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance); //避免日志中的中文输出乱码
        }
    }
}