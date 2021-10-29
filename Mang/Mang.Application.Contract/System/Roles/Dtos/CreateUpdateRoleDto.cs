using System.ComponentModel.DataAnnotations;
using Mang.Domain.Shared.System.Roles;
using Mang.Public.Dto;

namespace Mang.Application.Contract.System.Roles
{
    /// <summary>
    /// 创建角色业务实体
    /// </summary>
    public class CreateUpdateRoleDto : SortDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "角色名不能为空")]
        [MaxLength(RoleConst.MaxNameLength)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Required]
        [MaxLength(RoleConst.MaxDescriptionLength)]
        public string Description { get; set; }
    }
}