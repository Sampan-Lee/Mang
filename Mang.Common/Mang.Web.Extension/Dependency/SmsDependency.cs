using Microsoft.Extensions.DependencyInjection;
using Mang.Infrastructure.Sms;
using Mang.Public.Extension;
using Mang.Public.Util;

namespace Mang.Web.Extension.Dependency
{
    public static class SmsDependency
    {
        public static void AddSms(this IServiceCollection services)
        {
            var smsType = (SmsType) Appsettings.app("AppSettings", "Sms", "SmsType").ToInt();
            switch (smsType)
            {
                case SmsType.AliyunSms:
                    services.AddScoped<ISmsService, AliyunSms>();
                    break;
                case SmsType.TencentSms:
                    services.AddScoped<ISmsService, TencentSms>();
                    break;
                case SmsType.DevSms:
                default:
                    services.AddScoped<ISmsService, FakeSms>();
                    break;
            }
        }
    }
}