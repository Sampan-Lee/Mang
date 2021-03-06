using System.Collections.Generic;
using FreeSql.DataAnnotations;

namespace Mang.Domain.System
{
    /// <summary>
    /// 权限
    /// </summary>
    [Table(Name = "system_permission")]
    public class Permission : Entity
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        [Navigate(ManyToMany = typeof(RolePermission))]
        public virtual ICollection<Role> Roles { get; set; }

        [Navigate(nameof(ParentId))] public virtual Permission Parent { get; set; }

        [Navigate(nameof(ParentId))] public virtual ICollection<Permission> Children { get; set; }
    }
}