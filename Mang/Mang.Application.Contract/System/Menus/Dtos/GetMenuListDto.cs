using Mang.Public.Dto;

namespace Mang.Application.Contract.System.Menus
{
    /// <summary>
    /// 获取菜单列表业务实体
    /// </summary>
    public class GetMenuListDto : GetSortDto
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
    }
}