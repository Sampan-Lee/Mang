using System.ComponentModel;

namespace Mang.Infrastructure.ElasticSearch
{
    /// <summary>
    /// 对象选择
    /// </summary>
    public enum StatObjectCategory
    {
        /// <summary>
        /// 设备
        /// </summary>
        [Description("设备")] Device,

        /// <summary>
        /// 账号
        /// </summary>
        [Description("账号")] Account
    }
}