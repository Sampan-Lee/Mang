using Mang.Public.Dto;

namespace Mang.Application.Contract.Users
{
    /// <summary>
    /// 登录用户
    /// </summary>
    public class LoginUserDto
    {
        /// <summary>
        /// 授权码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string AvatarUrl { get; set; }
    }
}