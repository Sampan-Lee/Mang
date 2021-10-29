using System.Collections.Generic;
using System.Threading.Tasks;
using Mang.Application.Contract.System.Admins;
using Mang.ServiceBase.Interface;

namespace Mang.Application.Contract.System.Accounts
{
    /// <summary>
    /// 管理员用户服务接口
    /// </summary>
    public interface IAccountService : IBaseService
    {
        /// <summary>
        /// 发送登录验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        Task<bool> SendCaptchaAsync(string phone);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AccountDto> LoginAsync(LoginDto input);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        Task<AdminDto> GetUserAsync();

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <returns></returns>
        Task<List<AccountMenuDto>> GetMenuAsync();

        /// <summary>
        /// 获取用户操作权限
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetPermissionAsync();

        /// <summary>
        /// 校验账号权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<bool> CheckPermissionAsync(int userId, string permission);

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        Task LogoutAsync();
    }
}