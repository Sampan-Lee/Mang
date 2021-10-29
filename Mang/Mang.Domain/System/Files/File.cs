using System;
using FreeSql.DataAnnotations;

namespace Mang.Domain.System
{
    /// <summary>
    /// 文件
    /// </summary>
    [Table(Name = "system_file")]
    public class File : CreateEntity
    {
        /// <summary>
        /// 编码
        /// </summary>
        public Guid Code { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
    }
}