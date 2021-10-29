using Mang.Public.Dto;

namespace Mang.Application.Contract.System.Menus
{
    /// <summary>
    /// 绑定权限业务实体
    /// </summary>
    public class BindPermissionDto : Dto
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public int? PermissionId { get; set; }
    }
}