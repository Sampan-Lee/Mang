using System.Collections.Generic;
using FreeSql.DataAnnotations;
using JetBrains.Annotations;

namespace Mang.Domain.System
{
    /// <summary>
    /// 角色实体
    /// </summary>
    [Table(Name = "system_role")]
    public class Role : SortEnableBaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        [Navigate(ManyToMany = typeof(AdminRole))]
        public virtual ICollection<Admin> Users { get; set; }

        [Navigate(ManyToMany = typeof(RolePermission))]
        public virtual ICollection<Permission> Permissions { get; set; }

        internal Role ChangeName([NotNull] string name)
        {
            Name = name;
            return this;
        }
    }
}