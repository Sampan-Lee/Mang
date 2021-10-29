using Mang.Public.Util;

namespace Mang.Domain.System
{
    public class CaptchaWrongException : BusinessException
    {
        public CaptchaWrongException()
            : base("验证码错误")
        {
        }
    }
}