using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mang.Application.Contract.System.Admins
{
    /// <summary>
    /// 创建用户业务实体
    /// </summary>
    public class CreateUpdateAdminDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(ErrorMessage = "邮箱不能为空")]
        public string Email { get; set; }

        /// <summary>
        /// 角色ID集合
        /// </summary>
        public List<int> RoleIds { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        
        public int Sort { get; set; }
    }
}