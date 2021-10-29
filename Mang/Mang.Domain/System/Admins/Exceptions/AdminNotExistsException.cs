using Mang.Public.Util;

namespace Mang.Domain.System
{
    public class AdminNotExistsException : BusinessException
    {
        public AdminNotExistsException(string identification)
            : base($"用户不存在：{identification}")
        {
        }
    }
}