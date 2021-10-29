using System.ComponentModel;

namespace Mang.Infrastructure.ElasticSearch
{
    /// <summary>
    /// vip等级
    /// </summary>
    public enum StatLevel
    {
        [Description("一级")] Level1 = 1,
        [Description("二级")] Level2 = 2,
        [Description("三级")] Level3 = 3,
        [Description("四级")] Level4 = 4,
        [Description("五级")] Level5 = 5,
    }
}