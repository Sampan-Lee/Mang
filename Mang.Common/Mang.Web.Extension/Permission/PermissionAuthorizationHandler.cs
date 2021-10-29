using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using IdentityModel;
using Mang.Application.Contract.System.Accounts;
using Mang.Public.CurrentUser;
using Mang.Public.Extension;

namespace Mang.Web.Extension.Permission
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        private readonly IAccountService _accountService;

        public PermissionAuthorizationHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionAuthorizationRequirement requirement)
        {
            var userId = context.User.FindFirst(a => a.Type == JwtClaimTypes.Subject)?.Value;

            if (!userId.IsNullOrWhiteSpace())
            {
                var isSuperAdmin = context.User.IsSuperAdmin();
                if (isSuperAdmin.HasValue && isSuperAdmin.Value)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    if (context.User.IsAdminUser()!.Value)
                    {
                        var check = _accountService.CheckPermissionAsync(int.Parse(userId!), requirement.Name).Result;
                        if (check)
                        {
                            context.Succeed(requirement);
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}