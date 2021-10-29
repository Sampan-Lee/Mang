using System.Collections.Generic;

namespace Mang.Public.Dto
{
    /// <summary>
    /// 分页结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResultDto<T> where T : class
    {
        /// <summary>
        /// 总数
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> Data { get; set; } = default(List<T>);
    }
}