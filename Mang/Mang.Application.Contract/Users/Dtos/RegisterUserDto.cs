using Game.Domain.Shared;

namespace Mang.Application.Contract.Users
{
    /// <summary>
    /// 用户完成注册参数
    /// </summary>
    public class RegisterUserDto
    {
        /// <summary>
        /// 微信号
        /// </summary>
        public string WechatNum { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 兴趣/爱好
        /// </summary>
        public string Hobby { get; set; }
    }
}