using Mang.Public.Dto;

namespace Mang.Application.Contract.Users
{
    /// <summary>
    /// 登录参数
    /// </summary>
    public class UserLoginDto : Dto
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 是否VIP
        /// </summary>
        public bool IsVip { get; set; }

        /// <summary>
        /// 是否完成注册
        /// </summary>
        public bool IsFinishRegister { get; set; }
    }
}