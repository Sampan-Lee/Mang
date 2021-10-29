using Mang.Public.Util;

namespace Mang.Domain.System
{
    public class PasswordWrongException : BusinessException
    {
        public PasswordWrongException()
            : base("密码错误")
        {
        }
    }
}