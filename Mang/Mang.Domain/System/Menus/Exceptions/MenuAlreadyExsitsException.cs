using Mang.Public.Util;

namespace Mang.Domain.System
{
    public class MenuAlreadyExsitsException : BusinessException
    {
        public MenuAlreadyExsitsException(string name)
            : base($"菜单已存在：{name}")
        {
        }
    }
}