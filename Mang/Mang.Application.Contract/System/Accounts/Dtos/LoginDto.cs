using System.ComponentModel.DataAnnotations;

namespace Mang.Application.Contract.System.Accounts
{
    /// <summary>
    /// 登录请求参数
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        [Required]
        public string Identifer { get; set; }

        /// <summary>
        /// 登录凭据
        /// </summary>
        [Required]
        public string Credential { get; set; }

        /// <summary>
        /// 登录方式
        /// </summary>
        [Required]
        public LoginType Type { get; set; }
    }

    /// <summary>
    /// 登录方式
    /// </summary>
    public enum LoginType
    {
        /// <summary>
        /// 账号登录
        /// </summary>
        Account,

        /// <summary>
        /// 手机号登录
        /// </summary>
        Phone
    }
}