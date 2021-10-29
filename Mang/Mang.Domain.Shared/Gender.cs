using System.ComponentModel;

namespace Game.Domain.Shared
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        [Description("男")] Man,
        [Description("女")] Woman
    }
}