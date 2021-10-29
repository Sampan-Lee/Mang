using Mang.Public.Dto;

namespace Mang.Application.Contract.System.Roles
{
    /// <summary>
    /// 角色列表业务实体
    /// </summary>
    public class RoleListDto : SortEnableBaseDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}