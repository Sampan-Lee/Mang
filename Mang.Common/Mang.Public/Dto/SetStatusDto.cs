using System.ComponentModel.DataAnnotations;

namespace Mang.Public.Dto
{
    /// <summary>
    /// 设置状态参数（MongoDB，没有自定义权限）
    /// </summary>
    public class SetStatusDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// 状态 有效/无效
        /// </summary>
        [Required]
        public bool Status { get; set; }
    }
}