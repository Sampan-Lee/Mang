using Microsoft.AspNetCore.Authorization;

namespace Mang.Web.Extension.Permission
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Name { get; }

        public PermissionAuthorizationRequirement(string name)
        {
            Name = name;
        }
    }
}