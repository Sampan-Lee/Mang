using System.ComponentModel.DataAnnotations;

namespace Mang.Public.Dto
{
    /// <summary>
    /// 设置启用/禁用状态（MySql自定义的）
    /// </summary>
    public class SetIsEnableDto
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public bool IsEnable { get; set; }
    }
}