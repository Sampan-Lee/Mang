using System;
using Game.Domain.Shared;

namespace Mang.Domain.Users
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// 微信平台OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WechatNum { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 年龄段ID
        /// </summary>
        public int AgeBracketId { get; set; }

        /// <summary>
        /// 兴趣/爱好
        /// </summary>
        public string Hobby { get; set; }

        /// <summary>
        /// 完成注册 0：未完成；1：已完成
        /// </summary>
        public bool IsFinishRegister { get; set; }

        /// <summary>
        /// 创建/注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
    }
}