using System.Collections.Generic;
using System.Threading.Tasks;
using Mang.ServiceBase.Interface;
using Mang.Public.Dto;

namespace Mang.Application.Contract.System.Logs
{
    /// <summary>
    /// 系统日志服务接口
    /// </summary>
    public interface ILogService : IReadService<LogDto, LogListDto, GetLogListDto>
    {
        /// <summary>
        /// 获取日志等级字典
        /// </summary>
        /// <returns></returns>
        Task<List<DictionaryDto>> GetLevelDictionaryAsync();
    }
}