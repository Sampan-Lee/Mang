using FreeSql.DataAnnotations;

namespace Mang.Domain.System
{
    [Table(Name = "system_admin_role")]
    public class AdminRole : Entity
    {
        public int AdminId { get; set; }

        public virtual Admin Admin { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}