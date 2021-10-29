using System.Collections.Generic;
using Mang.Public.Dto;

namespace Mang.Application.Contract.System.Admins
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class AdminDto : BaseDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public List<int> RoleIds { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}