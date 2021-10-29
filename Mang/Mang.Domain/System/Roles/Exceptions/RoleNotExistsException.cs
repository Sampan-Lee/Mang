using Mang.Public.Util;

namespace Mang.Domain.System
{
    public class RoleNotExistsException : BusinessException
    {
        public RoleNotExistsException(string identification)
            : base($"角色不存在：{identification}")
        {
        }
    }
}