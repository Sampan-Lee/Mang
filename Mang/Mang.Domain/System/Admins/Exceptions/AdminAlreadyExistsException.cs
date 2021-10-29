using Mang.Public.Util;

namespace Mang.Domain.System
{
    public class AdminAlreadyExistsException : BusinessException
    {
        public AdminAlreadyExistsException(string name)
            : base($"用户已存在：{name}")
        {
        }
    }
}