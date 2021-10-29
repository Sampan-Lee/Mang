using System.Collections.Generic;
using Mang.Public.Dto;

namespace Mang.Application.Contract.System.Admins
{
    /// <summary>
    /// 用户列表
    /// </summary>
    public class AdminListDto : SortEnableBaseDto
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public List<string> Roles { get; set; }
    }
}