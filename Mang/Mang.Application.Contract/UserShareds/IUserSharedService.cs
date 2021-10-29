using System.Threading.Tasks;
using Mang.ServiceBase.Interface;

namespace Mang.Application.Contract.UserShareds
{
    /// <summary>
    /// 用户分享服务接口
    /// </summary>
    public interface IUserSharedService : IBaseService
    {
        /// <summary>
        /// 添加一条用户分享数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(CreateUserSharedDto input);
    }
}