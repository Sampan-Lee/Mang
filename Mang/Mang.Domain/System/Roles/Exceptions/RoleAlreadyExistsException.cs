using Mang.Public.Util;

namespace Mang.Domain.System
{
    public class RoleAlreadyExistsException : BusinessException
    {
        public RoleAlreadyExistsException(string name)
            : base($"角色名已存在：{name}")
        {
        }
    }
}