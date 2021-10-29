using System.ComponentModel;

namespace Mang.Infrastructure.ElasticSearch
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum StatUserCategory
    {
        /// <summary>
        /// 新用户
        /// </summary>
        [Description("新用户")] New,

        /// <summary>
        /// 老用户
        /// </summary>
        [Description("老用户")] Old
    }
}