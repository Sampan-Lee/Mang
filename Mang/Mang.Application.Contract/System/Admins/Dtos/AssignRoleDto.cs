using System.Collections.Generic;

namespace Mang.Application.Contract.System.Admins
{
    /// <summary>
    /// 分配角色业务实体
    /// </summary>
    public class AssignRoleDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色ID集合
        /// </summary>
        public List<int> RoleIds { get; set; }
    }
}