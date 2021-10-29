using System;
using FreeSql.DataAnnotations;

namespace Mang.Domain.Users
{
    /// <summary>
    /// 用户权限
    /// </summary>
    [Table(Name = "user_permission")]
    public class UserPermission : Entity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Navigate(nameof(UserId))]
        public virtual User User { get; set; }

        /// <summary>
        /// 是否充值
        /// </summary>
        public bool IsVip { get; set; }

        /// <summary>
        /// 会员周期开始日期
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// 会员周期截止日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 剩余获取纸条次数，IsVip为0时使用
        /// </summary>
        public int CanGetTimes { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}