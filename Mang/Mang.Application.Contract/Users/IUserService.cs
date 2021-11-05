using System.Threading.Tasks;
using Mang.ServiceBase.Interface;

namespace Mang.Application.Contract.Users
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserLoginDto> LoginAsync(LoginDto input);

        /// <summary>
        /// 补充用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserLoginDto> CompleteAsync(CompleteUserDto input);
    }
}