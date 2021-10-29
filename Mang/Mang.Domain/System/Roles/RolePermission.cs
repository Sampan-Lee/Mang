using FreeSql.DataAnnotations;

namespace Mang.Domain.System
{
    [Table(Name = "system_role_permission")]
    public class RolePermission : Entity
    {
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
    }
}