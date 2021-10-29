using System;
using Mang.Public.Dto;

namespace Mang.Application.Contract.System.Logs
{
    /// <summary>
    /// 获取日志列表条件
    /// </summary>
    public class GetLogListDto : GetPageDto
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 请求接口
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 解决方案名称
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// 请求链路ID
        /// </summary>
        public string TraceId { get; set; }

        /// <summary>
        /// 请求链路ID
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 请求链路ID
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        public string Message { get; set; }
    }
}