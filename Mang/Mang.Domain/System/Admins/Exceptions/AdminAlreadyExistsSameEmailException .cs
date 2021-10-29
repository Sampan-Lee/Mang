using System;
using Mang.Public.Util;

namespace Mang.Domain.System
{
    public class AdminAlreadyExistsSameEmailException : BusinessException
    {
        public AdminAlreadyExistsSameEmailException(string email)
            : base($"邮箱已存在：{email}")
        {
        }
    }
}