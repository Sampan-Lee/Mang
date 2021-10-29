using Mang.Public.Util;

namespace Mang.Domain.System
{
    public class AdminAlreadyExistsSamePhoneException : BusinessException
    {
        public AdminAlreadyExistsSamePhoneException(string phone)
            : base($"手机号已存在：{phone}")
        {
        }
    }
}