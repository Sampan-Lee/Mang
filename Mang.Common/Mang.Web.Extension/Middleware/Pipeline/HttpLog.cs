using Microsoft.AspNetCore.Builder;

namespace Mang.Web.Extension.Middleware.Pipeline
{
    public static class HttpLog
    {
        /// <summary>
        /// 请求响应中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static void UseHttpLog(this IApplicationBuilder app)
        {
            app.UseMiddleware<HttpLogMiddleware>();
        }
    }
}