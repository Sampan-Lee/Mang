using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Mang.Infrastructure;
using Mang.Public.Extension;
using Mang.Public.Util;

namespace Mang.Web.Extension.Middleware
{
    /// <summary>
    /// 中间件
    /// 记录请求和响应数据
    /// </summary>
    public class HttpLogMiddleware
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public HttpLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //记录上线文请求的唯一表示 这个中间件比较靠前目前记录在这里
            Trace.CorrelationManager.ActivityId = Guid.NewGuid();
            //初始化时间
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            #region 执行逻辑

            if (Appsettings.app("Middleware", "HttpLog").ToBool() && context.Request.Method.ToUpper() != "GET")
            {
                context.Request.EnableBuffering();
                Stream originalBody = context.Response.Body;

                try
                {
                    // 存储请求数据
                    await RequestDataLog(context);

                    using (var ms = new MemoryStream())
                    {
                        context.Response.Body = ms;

                        await _next(context);

                        // 存储响应数据
                        ResponseDataLog(context.Response, ms);
                        ms.Position = 0;
                        await ms.CopyToAsync(originalBody);

                        stopwatch.Stop();
                        if (stopwatch.ElapsedMilliseconds > 5000)
                        {
                            Console.WriteLine($"请求超时：{context.Request.Path}");
                        }

                        LogHelper.Info($"执行耗时：{stopwatch.ElapsedMilliseconds}ms");
                    }
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    // 记录异常
                    LogHelper.Error(ex.Message);
                }
                finally
                {
                    context.Response.Body = originalBody;
                }
            }
            else
            {
                await _next(context);
                stopwatch.Stop();
            }

            #endregion
        }

        private async Task RequestDataLog(HttpContext context)
        {
            var request = context.Request;
            var sr = new StreamReader(request.Body);
            var bodyData = await sr.ReadToEndAsync();
            bodyData = !string.IsNullOrWhiteSpace(bodyData) ? bodyData : "BodyData：bodyData";
            var content = $" QueryData：{request.Path + request.QueryString}\r\n {bodyData}";

            if (!string.IsNullOrEmpty(content))
            {
                Parallel.For(0, 1, _ => { LogHelper.Info(String.Join("\r\n", "请求数据：", content)); });
                request.Body.Position = 0;
            }
        }

        // 返回的数据暂时关闭掉不显示
        private void ResponseDataLog(HttpResponse response, MemoryStream ms)
        {
            ms.Position = 0;
            var responseBody = new StreamReader(ms).ReadToEnd();

            // 去除 Html
            var reg = "<[^>]+>";
            var isHtml = Regex.IsMatch(responseBody, reg);

            if (!string.IsNullOrEmpty(responseBody))
            {
                Parallel.For(0, 1, e => { LogHelper.Info(String.Join("\r\n", "响应数据：", responseBody)); });
            }
        }
    }
}