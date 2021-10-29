using System;
using Mang.Public.Dto;

namespace Mang.Application.Contract.System.Files
{
    /// <summary>
    /// 文件
    /// </summary>
    public class FileDto : CreateDto
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