using System.Collections.Generic;
using Mang.Public.Dto;

namespace Mang.Application.Contract.System.Permissions
{
    /// <summary>
    /// 菜单权限
    /// </summary>
    public class MenuPermissionDto
    {
        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 菜单权限
        /// </summary>
        public List<DictionaryDto> Permissions { get; set; }
    }
}