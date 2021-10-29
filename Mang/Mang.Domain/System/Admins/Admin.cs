using System.Collections.Generic;
using FreeSql.DataAnnotations;
using JetBrains.Annotations;
using Mang.Public.Util;

namespace Mang.Domain.System
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Table(Name = "system_admin")]
    public class Admin : SortEnableBaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 密码盐
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 用户角色信息
        /// </summary>
        [Navigate(ManyToMany = typeof(AdminRole))]
        public virtual ICollection<Role> Roles { get; set; }

        internal Admin ChangeName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
            return this;
        }

        internal Admin ChangePhone([NotNull] string phone)
        {
            Phone = Check.NotNullOrWhiteSpace(phone, nameof(phone));
            return this;
        }

        internal Admin ChangeEmail([NotNull] string email)
        {
            Email = Check.NotNullOrWhiteSpace(email, nameof(email));
            return this;
        }
    }
}