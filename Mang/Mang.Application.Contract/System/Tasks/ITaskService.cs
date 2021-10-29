using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mang.Application.Contract.Tasks
{
    /// <summary>
    /// 任务服务接口
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<TaskDto>> GetAll();
    }
}