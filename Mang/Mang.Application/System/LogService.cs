using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeSql;
using Mang.Domain.System;
using Mang.Application.Contract.System.Logs;
using Mang.ServiceBase.Impl;
using Mang.Infrastructure.Repository;
using Mang.Public.Dto;
using NLog;

namespace Mang.Application.System
{
    /// <summary>
    /// 日志服务（只读）
    /// </summary>
    public class LogService : ReadSerivce<Log, LogDto, LogListDto, GetLogListDto>,
        ILogService
    {
        public LogService(IRepository<Log> repository) : base(repository)
        {
        }

        /// <summary>
        /// 查询构造器
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override ISelect<Log> CreateFilteredQuery(GetLogListDto input)
        {
            return Repository.Select.LeftJoin(
                    @"(select `TraceId`,MIN(`CreateTime`) as CreateTime from `system_log` group by `TraceId`) 
                            b on a.`TraceId`=b.`TraceId`")
                .Include(a => a.User)
                .Where(a => !string.IsNullOrWhiteSpace(a.TraceId))
                .WhereIf(!string.IsNullOrWhiteSpace(input.RequestUrl), a => a.RequestUrl.Contains(input.RequestUrl))
                .WhereIf(input.BeginTime.HasValue, a => a.CreateTime >= input.BeginTime.Value)
                .WhereIf(input.EndTime.HasValue, a => a.CreateTime <= input.EndTime.Value)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Application), a => a.Application.Contains(input.Application))
                .WhereIf(!string.IsNullOrWhiteSpace(input.TraceId), a => a.TraceId.Contains(input.TraceId))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Level), a => a.Level == input.Level)
                .WhereIf(!string.IsNullOrWhiteSpace(input.UserName), a => a.User.Name.Contains(input.UserName))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Message), a => a.Message.Contains(input.Message))
                .OrderBy($"b.CreateTime {(input.Asc ? "asc" : "desc")}")
                .OrderBy(a => a.Id);
        }

        /// <summary>
        /// 列表映射
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        protected override LogListDto MapToListDto(Log log)
        {
            var logListDto = base.MapToListDto(log);
            logListDto.UserName = log.User?.Name;
            return logListDto;
        }

        /// <summary>
        /// 日志级别字典
        /// </summary>
        /// <returns></returns>
        public async Task<List<DictionaryDto>> GetLevelDictionaryAsync()
        {
            var result = await Task.FromResult(
                LogLevel.AllLoggingLevels.Select(a => new DictionaryDto
                {
                    Key = a.Ordinal,
                    Value = a.Name
                }).ToList()
            );

            return result;
        }
    }
}