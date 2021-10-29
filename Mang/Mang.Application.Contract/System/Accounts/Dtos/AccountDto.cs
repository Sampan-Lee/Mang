using Mang.Public.Dto;

namespace Mang.Application.Contract.System.Accounts
{
    /// <summary>
    /// 登录用户数据
    /// </summary>
    public class AccountDto : BaseDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
    }
}