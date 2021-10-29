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
        /// <param name="code"></param>
        /// <returns></returns>
        Task<UserLoginDto> LoginAsync(string code);

        /// <summary>
        /// 完成用户注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> RegisterAsync(RegisterUserDto input);
    }
}