using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Mang.Public.Extension;

namespace Mang.Public.CurrentUser
{
    public class CurrentUser : ICurrentUser
    {
        private readonly ClaimsPrincipal _claimsPrincipal;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _claimsPrincipal = httpContextAccessor.HttpContext?.User ?? Thread.CurrentPrincipal as ClaimsPrincipal;
        }

        public int Id => _claimsPrincipal.FindUserId();
        public string Name => _claimsPrincipal.FindUserName();
        public string Phone => _claimsPrincipal.FindPhone();
        public string Avatar => _claimsPrincipal.FindAvatar();
        public bool? IsAdmin => _claimsPrincipal.IsAdminUser();
        public List<int> Roles => FindClaims(ClaimTypes.Role).Select(c => c.Value.ToInt()).ToList();

        public Claim FindClaim(string claimType)
        {
            return _claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == claimType);
        }

        public List<Claim> FindClaims(string claimType)
        {
            return _claimsPrincipal?.Claims.Where(c => c.Type == claimType).ToList();
        }

        public List<Claim> GetAllClaims()
        {
            return _claimsPrincipal?.Claims.ToList();
        }

        public bool HasRole(int roleId)
        {
            return Roles.Contains(roleId);
        }
    }
}